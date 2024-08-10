using AutoMapper;
using BookAPI.Models;
using BookAPI.Models.DTO;
using BookAPI.Repository;
using BookAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;



namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IPublisherRepository _publisherRepository;
        private readonly ICategoryRepository _categoryRepository;
        public APIController(IBookRepository bookRepository, IMapper mapper, IPublisherRepository publisherRepository,
            ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            this._response = new();
            _publisherRepository = publisherRepository;
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(CacheProfileName ="Default30")]
        public async Task<ActionResult<APIResponse>> GetAllBooks([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Book> bookList = await _bookRepository.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                if(!string.IsNullOrEmpty(search))
                {
                    bookList = bookList.Where(b => b.Name.ToLower().Contains(search));
                }
                Pagination pagination = new()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagination));  
                _response.Result = _mapper.Map<List<BookDTO>>(bookList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString()};
            }
            return _response;

        }
        [HttpGet("{id:int}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetBook(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var book = await _bookRepository.GetAsync(u => u.Id == id);
                if (book == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<BookDTO>(book);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> CreateBook([FromBody] BookCreateDTO bookDTO)
        {
            try
            {
                if (await _bookRepository.GetAsync(u => u.Name.ToLower() == bookDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Book Already Exists!");
                    return BadRequest(ModelState);
                }
                if (bookDTO == null)
                {
                    return BadRequest(bookDTO);
                }
             
                Book model = _mapper.Map<Book>(bookDTO);
                var publisher = await _publisherRepository.GetAsync(p => p.Id == model.PublisherId);
                if (publisher == null)
                {
                    ModelState.AddModelError("CustomError", "Invalid PublisherId!");

                    return BadRequest(ModelState);
                }
                if(await _categoryRepository.GetAsync(u => u.Id == bookDTO.CategoryId) == null)
                {
                    ModelState.AddModelError("CustomError", "Invalid CategoryId!");
                    return BadRequest(ModelState);
                }
                model.CreatedDate = DateTime.Now;
                await _bookRepository.CreateAsync(model);
                await _bookRepository.SaveAsync();
                _response.Result = _mapper.Map<BookDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetBook", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;


           
        }
        [HttpDelete("{id:int}",Name = "DeleteBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public async Task <ActionResult<APIResponse>> DeleteBook(int id)
        {
            try
            {
                if (id == 0)
                { 
                    return BadRequest();
                }
                var book = await _bookRepository.GetAsync(u => u.Id == id);

                if (book == null)
                {
                    return NotFound();
                }
                await _bookRepository.RemoveAsync(book);
                await _bookRepository.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;


        }
        [HttpPut("{id:int}",Name ="UpdateBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]


        public async Task<ActionResult<APIResponse>> UpdateBook (int id, [FromBody] BookUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                Book model = _mapper.Map<Book>(updateDTO);
                var publisher = await _publisherRepository.GetAsync(p => p.Id == model.PublisherId);
                if (publisher == null)
                {
                    ModelState.AddModelError("CustomError", "Invalid PublisherId!");
                    return BadRequest(ModelState);
                }
                await _bookRepository.UpdateAsync(model);
                await _bookRepository.SaveAsync();
                _response.StatusCode =HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;


            

        }
        [HttpGet("category={categoryId:int}", Name = "GetBookByCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetBookByCategory(int categoryId)
        {
            try
            {
                if (categoryId == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var book = await _bookRepository.GetAllAsync(u => u.CategoryId == categoryId);
                if(book.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<List<BookDTO>>(book);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpGet("publisher={publisherId:int}", Name = "GetBookByPublisher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetBookByPublisher(int publisherId)
        {
            try
            {
                if (publisherId == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var book = await _bookRepository.GetAllAsync(u => u.PublisherId == publisherId);
              
                if (book.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<List<BookDTO>>(book);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }

    }
}
