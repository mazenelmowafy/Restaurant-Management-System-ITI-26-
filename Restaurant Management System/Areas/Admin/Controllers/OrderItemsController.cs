using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;

namespace RestaurantManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/OrderItems
        public IActionResult Index()
        {
            var orderItems = _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .OrderByDescending(oi => oi.OrderId)
                .ToList();

            return View(orderItems);
        }
    }
}