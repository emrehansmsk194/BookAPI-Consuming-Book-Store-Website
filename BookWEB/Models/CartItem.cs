namespace BookWEB.Models
{
    public class CartItem
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}
