using AutoMapper;
using BookAPI_Utility;
using BookWEB.Models;
using BookWEB.Models.DTO;
using BookWEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace BookWEB.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly IPublisherService _publisherService;
        public CategoryController(ICategoryService categoryService, IBookService bookService, IMapper mapper,
            IPublisherService publisherService ) : base(categoryService,publisherService)
        {
            _categoryService = categoryService;
            _bookService = bookService;
            _mapper = mapper;
            _publisherService = publisherService;
        }
        public async Task<IActionResult> IndexCategory()
        {
            List<CategoryDTO> categoryList = new();
            var response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                categoryList = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(response.Result));
                
            }
            return View(categoryList);
        }
      
    }
}
