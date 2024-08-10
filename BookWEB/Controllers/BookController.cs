using AutoMapper;
using BookAPI_Utility;
using BookWEB.Models;
using BookWEB.Models.DTO;
using BookWEB.Models.VM;
using BookWEB.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BookWEB.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;
        public BookController(IBookService bookService, IMapper mapper, ICategoryService categoryService,
            IPublisherService publisherService) : base(categoryService,publisherService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _mapper = mapper;
            _publisherService = publisherService;
        }
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> IndexBook()
        {
            List<BookDTO> bookList = new();
            var response = await _bookService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                bookList = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
                foreach (var book in bookList)
                {
                    var categoryResponse = await _categoryService.GetAsync<APIResponse>(book.CategoryId, HttpContext.Session.GetString(SD.SessionToken));
                    if (categoryResponse != null && categoryResponse.IsSuccess)
                    {
                        var category = JsonConvert.DeserializeObject<CategoryDTO>(Convert.ToString(categoryResponse.Result));
                        book.CategoryName = category.Name;
                    }

                }
            }
            return View(bookList);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateBook()
        {
            BookCreateVM bookCreateVM = new();
            List<BookDTO> bookList = new();
            var response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                bookCreateVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return View(bookCreateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateBook(BookCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookService.CreateAsync<APIResponse>(model.Book, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Book created successfully.";
                    return RedirectToAction(nameof(IndexBook));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CategoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBook(int id)
        {
            BookUpdateVM bookVM = new();
            var response = await _bookService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                BookDTO model = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                bookVM.Book = _mapper.Map<BookUpdateDTO>(model);
            }
            response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                bookVM.CategoryList = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(bookVM);
            }
            return NotFound();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBook(BookUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookService.UpdateAsync<APIResponse>(model.Book, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Book updated successfully.";
                    return RedirectToAction(nameof(IndexBook));
                }
                else
                {
                    if(response.ErrorMessages.Count >0)
                    {
                        ModelState.AddModelError("ErrorMessages",response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if(resp != null && resp.IsSuccess)
            {
                model.CategoryList = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> BookByCategory(int categoryId)
        {
            List<BookDTO> books = new();
            var response = await _bookService.GetByCategoryAsync<APIResponse>(categoryId,HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                books = JsonConvert.DeserializeObject<List<BookDTO>>(JsonConvert.SerializeObject(response.Result));
            }
            return View(books);

        }
        [HttpGet]
        public async Task<IActionResult> BookByPublisher(int publisherId)
        {
            List<BookDTO> books = new();
            var response = await _bookService.GetByPublisherAsync<APIResponse>(publisherId, HttpContext.Session.GetString(SD.SessionToken));
            if(response != null && response.IsSuccess)
            {
                books = JsonConvert.DeserializeObject<List<BookDTO>>(JsonConvert.SerializeObject(response.Result));
            }
            return View(books);
        }
    }
}