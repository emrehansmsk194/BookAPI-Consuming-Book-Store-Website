using System.ComponentModel.DataAnnotations;

namespace BookWEB.Models.DTO
{
    public class CategoryCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
