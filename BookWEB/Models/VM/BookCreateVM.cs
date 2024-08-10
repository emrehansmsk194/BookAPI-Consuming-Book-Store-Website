using BookWEB.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWEB.Models.VM
{
	public class BookCreateVM
	{
        public BookCreateVM()
        {
            Book = new BookCreateDTO();
        }
        public BookCreateDTO Book {  get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
