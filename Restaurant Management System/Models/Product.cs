namespace RestaurantManagementSystem.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        public string? ImageFileName { get; set; }
    }
}
