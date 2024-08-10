using BookAPI_Utility;
using BookWEB.Models;
using BookWEB.Models.DTO;
using BookWEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace BookWEB.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPublisherService _publisherService;
        public BaseController(ICategoryService categoryService, IPublisherService publisherService)
        {
            _categoryService = categoryService;
            _publisherService = publisherService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

          
            
            
                var response = _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken)).GetAwaiter().GetResult();
                if (response != null && response.IsSuccess)
                {
                    var categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(response.Result));
                    ViewData["Categories"] = categories;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Failed to load categories from API.");
                }
                var publisherResponse = _publisherService.GetAllAsync<APIResponse>().GetAwaiter().GetResult();
                if(publisherResponse != null && publisherResponse.IsSuccess)
            {
                var publishers = JsonConvert.DeserializeObject<List<PublisherDTO>>(Convert.ToString(publisherResponse.Result));
                ViewData["Publishers"] = publishers;
            }
           
        }
    }
}
