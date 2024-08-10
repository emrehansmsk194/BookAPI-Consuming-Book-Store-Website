using AutoMapper;
using BookAPI_Utility;
using BookWEB.Models;
using BookWEB.Models.DTO;
using BookWEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BookWEB.Controllers
{
    public class HomeController : BaseController
    {

        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public HomeController(IBookService bookService, IMapper mapper,IPublisherService publisherService, ICategoryService categoryService):base(categoryService,publisherService)
        {
            _bookService = bookService;
            _mapper = mapper;
            _publisherService = publisherService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string search = "")
        {
            List<BookDTO> bookList = new();
            var response = await _bookService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                bookList = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(response.Result));
                if(!string.IsNullOrEmpty(search))
                {
                    bookList = bookList.Where(u => u.Name.ToLower().Contains(search.ToLower())).ToList();
                }
                foreach(var book in bookList)
                {
                    var publisherResponse = await _publisherService.GetAsync<APIResponse>(book.PublisherId);
                    if(publisherResponse != null && publisherResponse.IsSuccess)
                    {
                        var publisher = JsonConvert.DeserializeObject<PublisherDTO>(Convert.ToString(publisherResponse.Result));
                        book.PublisherName = publisher.Name;
                    }
                }
            }
            return View(bookList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexPost(string search)
        {
          return RedirectToAction("Index", new {search = search});


        }
    }
}
