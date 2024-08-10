using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models.DTO
{
    public class CategoryCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
