using System.ComponentModel.DataAnnotations;

namespace BookWEB.Models.DTO
{
    public class PublisherCreateDTO
    {
        [Required]
        public string Name  { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Range(1800, 2100)]
        public int YearEstablished { get; set; }
        [Phone]
        public string ContactNumber { get; set; }

      
    }
}
