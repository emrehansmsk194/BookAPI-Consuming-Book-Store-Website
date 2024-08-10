using AutoMapper;
using BookAPI.Models;
using BookAPI.Models.DTO;
using BookAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIPublisherController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IMapper _mapper;
        private readonly IPublisherRepository _dbPublisher;
        public APIPublisherController(IMapper mapper, IPublisherRepository dbPublisher)
        {
            _dbPublisher = dbPublisher;
            _mapper = mapper;
            this._response = new();
            
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllPublishers ()
        {
            try
            {
                IEnumerable<Publisher> publisher = await _dbPublisher.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<PublisherDTO>>(publisher);
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("{id:int}", Name = "GetPublisher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetPublisher(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.IsSuccess = false;
                    return BadRequest();
                }
                var publisher = await _dbPublisher.GetAsync(u =>u.Id == id);
                if (publisher == null)
                {
                    _response.IsSuccess=false;
                return NotFound(); 
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<PublisherDTO>(publisher);
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<APIResponse>> CreatePublisher ([FromBody] PublisherCreateDTO publisherDTO)
        {
            try
            {
                if (await _dbPublisher.GetAsync(u => u.Name == publisherDTO.Name) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Publisher already exists !");
                    return BadRequest(ModelState);
                }
                if (publisherDTO == null)
                {
                    return NotFound(publisherDTO);
                }
                Publisher model = _mapper.Map<Publisher>(publisherDTO);
                await _dbPublisher.CreateAsync(model);
                await _dbPublisher.SaveAsync();
                _response.Result = _mapper.Map<PublisherDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetPublisher", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        
        }
        [HttpDelete("{id:int}",Name ="DeletePublisher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeletePublisher (int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }
                var publisher = await _dbPublisher.GetAsync(u => u.Id == id);
                if (publisher == null)
                {
                    return NotFound();
                }
                await _dbPublisher.RemoveAsync(publisher);
                await _dbPublisher.SaveAsync();
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
        [HttpPut("{id:int}",Name ="UpdatePublisher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdatePublisher(int id, [FromBody] PublisherUpdateDTO publisherUpdateDTO)
        {
            try
            {
                if (publisherUpdateDTO == null || id != publisherUpdateDTO.Id)
                {
                    return BadRequest();
                }
                Publisher model = _mapper.Map<Publisher>(publisherUpdateDTO);
                await _dbPublisher.UpdateAsync(model);
                await _dbPublisher.SaveAsync();
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


    }
}
