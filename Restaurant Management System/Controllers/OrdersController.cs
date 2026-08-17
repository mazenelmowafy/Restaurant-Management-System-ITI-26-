using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.ViewModels;

namespace RestaurantManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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

            var orders = _context.Orders
                .Where(o =>
                    o.CustomerId == customerId.Value &&
                    o.Status != "Cart")
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        public IActionResult Create()
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

            var cart = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o =>
                    o.CustomerId == customerId.Value &&
                    o.Status == "Cart");


            var model = new OrderViewModel
            {
                CustomerId = customerId.Value,

                productIds = cart?.OrderItems
                    .Select(i => i.ProductId)
                    .ToList()
                    ?? new List<int>(),

                quantities = cart?.OrderItems
                    .Select(i => i.Quantity)
                    .ToList()
                    ?? new List<int>()
            };


            ViewBag.Cart = cart;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderViewModel model)
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


            var cart = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o =>
                    o.CustomerId == customerId.Value &&
                    o.Status == "Cart");


            if (cart == null)
                return NotFound();


            if (cart.OrderItems == null ||
                cart.OrderItems.Count == 0)
            {
                ModelState.AddModelError(
                    "",
                    "Cart is empty"
                );

                ViewBag.Cart = cart;
                return View(model);
            }


            decimal total = 0;


            foreach (var item in cart.OrderItems)
            {

                item.UnitPrice = item.Product.Price;

                item.SubTotal =
                    item.Quantity * item.UnitPrice;

                total += item.SubTotal;
            }


            cart.TotalAmount = total;
            cart.OrderDate = DateTime.Now;
            cart.Status = "Pending";


            _context.SaveChanges();


            return RedirectToAction(
                nameof(Details),
                new { id = cart.OrderId }
            );
        }


        public IActionResult Details(int id)
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
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o =>
                    o.OrderId == id &&
                    o.CustomerId == customerId.Value &&
                    o.Status != "Cart");


            if (order == null)
                return NotFound();


            return View(order);
        }
    }
}