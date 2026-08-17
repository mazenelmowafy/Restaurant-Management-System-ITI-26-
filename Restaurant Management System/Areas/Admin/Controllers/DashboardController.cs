using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;

namespace RestaurantManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var totalCustomers = _context.Customers.Count();

            var totalProducts = _context.Products.Count();

            var totalOrders = _context.Orders.Count();

            var totalSales = _context.Orders
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;

            var recentOrders = _context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToList();

            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalSales = totalSales;
            ViewBag.RecentOrders = recentOrders;

            return View();
        }
    }
}