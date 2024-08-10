using System.ComponentModel.DataAnnotations.Schema;

namespace BookAPI.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
        public DateTime CreatedDate     { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
