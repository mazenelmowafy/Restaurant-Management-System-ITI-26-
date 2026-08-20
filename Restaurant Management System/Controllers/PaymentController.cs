using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.ViewModels;
using System.Security.Claims;

namespace RestaurantManagementSystem.Controllers
{
    [Authorize(Roles = "Customer")]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int orderId)
        {
            int customerId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = _context.Orders
                .FirstOrDefault(o =>
                    o.OrderId == orderId &&
                    o.CustomerId == customerId);

            if (order == null)
                return NotFound();

            // Already confirmed → cannot pay again
            if (order.Status != "Pending")
            {
                return RedirectToAction(
                    "Details",
                    "Orders",
                    new { id = order.OrderId });
            }

            var model = new PaymentViewModel
            {
                OrderId = order.OrderId,
                Amount = order.TotalAmount,
                PaymentMethod = ""
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PaymentViewModel model)
        {
            int customerId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = _context.Orders
                .FirstOrDefault(o =>
                    o.OrderId == model.OrderId &&
                    o.CustomerId == customerId);

            if (order == null)
                return NotFound();
            if (order.Status != "Pending")
            {
                return RedirectToAction(
                    "Details",
                    "Orders",
                    new { id = order.OrderId });
            }

            if (string.IsNullOrEmpty(model.PaymentMethod))
            {
                ModelState.AddModelError(
                    "PaymentMethod",
                    "Please select a payment method.");

                model.Amount = order.TotalAmount;

                return View(model);
            }

            order.Status = "Confirmed";

            _context.SaveChanges();

            TempData["PaymentSuccess"] =
                "Payment completed successfully!";

            return RedirectToAction(
                "Details",
                "Orders",
                new { id = order.OrderId });
        }
    }
}