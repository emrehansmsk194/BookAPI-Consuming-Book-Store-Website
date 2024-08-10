using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookAPI.Models
{
    public class Publisher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Url]
       
        public string Website { get; set; }
        [Range(1800,2100)]
        public int YearEstablished { get; set; }
        [Phone]
        public string ContactNumber { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public ICollection<Book> Books { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
