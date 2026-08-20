using Microsoft.AspNetCore.Authorization; // أضفنا دي
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.ViewModels;
using System.Security.Claims; // وأضفنا دي عشان نقرأ الـ ID

namespace RestaurantManagementSystem.Controllers
{
    // السطر ده هيحمي الكنترولر كله، ومش هيدخل هنا غير الـ Customer بس
    // ولو مش مسجل دخول هيرجعه للوج ان تلقائياً
    [Authorize(Roles = "Customer")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // استخراج الـ ID الخاص بالعميل من الـ Claims بدلاً من السشن
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var orders = _context.Orders
                .Where(o =>
                    o.CustomerId == customerId &&
                    o.Status != "Cart")
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        public IActionResult Create()
        {
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cart = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o =>
                    o.CustomerId == customerId &&
                    o.Status == "Cart");


            var model = new OrderViewModel
            {
                CustomerId = customerId,

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
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cart = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o =>
                    o.CustomerId == customerId &&
                    o.Status == "Cart");


            if (cart == null)
                return NotFound();


            if (cart.OrderItems == null || cart.OrderItems.Count == 0)
            {
                ModelState.AddModelError("", "Cart is empty");
                ViewBag.Cart = cart;
                return View(model);
            }


            decimal total = 0;

            foreach (var item in cart.OrderItems)
            {
                item.UnitPrice = item.Product.Price;
                item.SubTotal = item.Quantity * item.UnitPrice;
                total += item.SubTotal;
            }


            cart.TotalAmount = total;
            cart.OrderDate = DateTime.Now;
            cart.Status = "Pending";

            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = cart.OrderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IncreaseQuantity(int orderId, int productId)
        {
            int customerId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var item = _context.OrderItems
                .Include(i => i.Product)
                .Include(i => i.Order)
                .FirstOrDefault(i =>
                    i.OrderId == orderId &&
                    i.ProductId == productId &&
                    i.Order.CustomerId == customerId &&
                    i.Order.Status == "Cart");

            if (item == null)
                return NotFound();

            item.Quantity++;

            item.UnitPrice = item.Product.Price;
            item.SubTotal = item.Quantity * item.UnitPrice;

            item.Order.TotalAmount = _context.OrderItems
                .Where(i => i.OrderId == item.OrderId)
                .Sum(i => i.SubTotal);

            _context.SaveChanges();

            return RedirectToAction(nameof(Create));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DecreaseQuantity(int orderId, int productId)
        {
            int customerId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var item = _context.OrderItems
                .Include(i => i.Product)
                .Include(i => i.Order)
                .FirstOrDefault(i =>
                    i.OrderId == orderId &&
                    i.ProductId == productId &&
                    i.Order.CustomerId == customerId &&
                    i.Order.Status == "Cart");

            if (item == null)
                return NotFound();

            if (item.Quantity > 1)
            {
                item.Quantity--;

                item.UnitPrice = item.Product.Price;
                item.SubTotal = item.Quantity * item.UnitPrice;
            }

            item.Order.TotalAmount = _context.OrderItems
                .Where(i => i.OrderId == item.OrderId)
                .Sum(i => i.SubTotal);

            _context.SaveChanges();

            return RedirectToAction(nameof(Create));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int orderId, int productId)
        {
            int customerId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var item = _context.OrderItems
                .Include(i => i.Order)
                .FirstOrDefault(i =>
                    i.OrderId == orderId &&
                    i.ProductId == productId &&
                    i.Order.CustomerId == customerId &&
                    i.Order.Status == "Cart");

            if (item == null)
                return NotFound();

            _context.OrderItems.Remove(item);

            _context.SaveChanges();

            var cart = _context.Orders
                .FirstOrDefault(o =>
                    o.OrderId == orderId &&
                    o.CustomerId == customerId &&
                    o.Status == "Cart");

            if (cart != null)
            {
                cart.TotalAmount = _context.OrderItems
                    .Where(i => i.OrderId == orderId)
                    .Sum(i => i.SubTotal);

                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Create));
        }




        public IActionResult Details(int id)
        {
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o =>
                    o.OrderId == id &&
                    o.CustomerId == customerId &&
                    o.Status != "Cart");


            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}