using Microsoft.AspNetCore.Authorization; // تمت الإضافة
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.ViewModels;
using System.Security.Claims; // تمت الإضافة

namespace RestaurantManagementSystem.Controllers
{
    // حماية صفحة الدفع بحيث لا يدخلها إلا العميل المسجل دخوله
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
            // 1. استخراج الـ ID الخاص بالعميل من الـ Claims بدلاً من السشن
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // 2. البحث عن الطلب والتأكد أنه يخص نفس العميل
            var order = _context.Orders
                .FirstOrDefault(o =>
                    o.OrderId == orderId &&
                    o.CustomerId == customerId); // قمنا بإزالة .Value لأن المتغير أصبح int صريح

            if (order == null)
                return NotFound();

            // 3. تجهيز بيانات الدفع للواجهة
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