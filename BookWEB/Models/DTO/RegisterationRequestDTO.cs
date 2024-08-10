using System.ComponentModel.DataAnnotations;

namespace BookWEB.Models.DTO
{
    public class RegisterationRequestDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name {  get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; } = "customer";
    }
}
