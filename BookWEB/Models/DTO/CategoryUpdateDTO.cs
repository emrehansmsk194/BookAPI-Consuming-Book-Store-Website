using System.ComponentModel.DataAnnotations;

namespace BookWEB.Models.DTO
{
    public class CategoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
