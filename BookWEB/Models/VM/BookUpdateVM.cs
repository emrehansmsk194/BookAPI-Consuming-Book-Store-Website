using BookWEB.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWEB.Models.VM
{
    public class BookUpdateVM
    {
        public BookUpdateVM()
        {
            Book = new BookUpdateDTO();
        }
        public BookUpdateDTO Book {  get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
