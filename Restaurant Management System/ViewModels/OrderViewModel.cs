namespace RestaurantManagementSystem.ViewModels
{
    public class OrderViewModel
    {
        public int CustomerId { get; set; }
        public List<int> productIds { get; set; }
        public List<int> quantities { get; set; }
    }
}
