namespace RestaurantManagementSystem.ViewModels
{
    public class PaymentViewModel
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
