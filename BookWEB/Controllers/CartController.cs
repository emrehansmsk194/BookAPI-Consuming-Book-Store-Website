using BookAPI_Utility;
using BookWEB.Extensions;
using BookWEB.Models;
using BookWEB.Models.DTO;
using BookWEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookWEB.Controllers
{
    public class CartController : BaseController
    {
        private readonly IPublisherService _publisherService;
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        public CartController(IPublisherService publisherService, ICategoryService categoryService,
            IBookService bookService) : base(categoryService,publisherService)
        {
            _categoryService = categoryService;
            _publisherService = publisherService;   
            _bookService = bookService;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody]  int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(u => u.BookId == id);
            if(item == null)
            {
                var response = await _bookService.GetAsync<APIResponse>(id, HttpContext.Session.GetString(SD.SessionToken));
                if(response != null && response.IsSuccess)
                {
                    var book = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                    var cartItem = new CartItem
                    {
                        BookId = book.Id,
                        Name = book.Name,
                        Price = book.Price,
                        Quantity = 1,
                        ImageUrl = book.CoverImageUrl
                    };
                    cart.Add(cartItem);
                }
            }
            else
            {
                item.Quantity++;
            }
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return Json(new { success = true });
           
        }
        public IActionResult IndexCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
            
        }
    }
}
