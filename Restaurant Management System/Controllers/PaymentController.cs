using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.ViewModels;

namespace RestaurantManagementSystem.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int orderId)
        {
            var customerId =
                HttpContext.Session.GetInt32("CustomerId");

            if (customerId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }


            var order = _context.Orders
                .FirstOrDefault(o =>
                    o.OrderId == orderId &&
                    o.CustomerId == customerId.Value);

            if (order == null)
                return NotFound();


            var model = new PaymentViewModel
            {
                OrderId = order.OrderId,
                Amount = order.TotalAmount,
                PaymentMethod = ""
            };


            return View(model);
        }
    }
}