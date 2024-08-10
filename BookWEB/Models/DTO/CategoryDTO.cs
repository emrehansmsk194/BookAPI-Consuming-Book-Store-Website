using System.ComponentModel.DataAnnotations;

namespace BookWEB.Models.DTO
{
    public class CategoryDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
