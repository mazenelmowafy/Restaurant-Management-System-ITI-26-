using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.Customer)
                .Include(oi => oi.Product)
                .OrderByDescending(oi => oi.OrderId)
                .ThenBy(oi => oi.ProductId)
                .ToList();

            return View(items);
        }

        public IActionResult Details(int? orderId, int? productId)
        {
            if (orderId == null || productId == null)
                return NotFound();

            var item = _context.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.Customer)
                .Include(oi => oi.Product)
                .FirstOrDefault(oi =>
                    oi.OrderId == orderId &&
                    oi.ProductId == productId);

            if (item == null)
                return NotFound();

            return View(item);
        }

        public IActionResult Create()
        {
            LoadOrders();
            LoadProducts();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("OrderId,ProductId,Quantity,UnitPrice,SubTotal")]
            OrderItem orderItem)
        {
            ModelState.Remove(nameof(OrderItem.Order));
            ModelState.Remove(nameof(OrderItem.Product));

            if (!_context.Orders.Any(o => o.OrderId == orderItem.OrderId))
            {
                ModelState.AddModelError(
                    nameof(OrderItem.OrderId),
                    "Please select a valid order.");
            }

            if (!_context.Products.Any(p => p.ProductId == orderItem.ProductId))
            {
                ModelState.AddModelError(
                    nameof(OrderItem.ProductId),
                    "Please select a valid product.");
            }

            bool alreadyExists = _context.OrderItems.Any(oi =>
                oi.OrderId == orderItem.OrderId &&
                oi.ProductId == orderItem.ProductId);

            if (alreadyExists)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "This product already exists in this order.");
            }

            if (!ModelState.IsValid)
            {
                LoadOrders(orderItem.OrderId);
                LoadProducts(orderItem.ProductId);

                return View(orderItem);
            }

            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? orderId, int? productId)
        {
            if (orderId == null || productId == null)
                return NotFound();

            var item = _context.OrderItems.Find(orderId, productId);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int orderId,
            int productId,
            [Bind("OrderId,ProductId,Quantity,UnitPrice,SubTotal")]
            OrderItem orderItem)
        {
            if (orderId != orderItem.OrderId ||
                productId != orderItem.ProductId)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(OrderItem.Order));
            ModelState.Remove(nameof(OrderItem.Product));

            if (!ModelState.IsValid)
                return View(orderItem);

            var existingItem = _context.OrderItems.Find(orderId, productId);

            if (existingItem == null)
                return NotFound();

            existingItem.Quantity = orderItem.Quantity;
            existingItem.UnitPrice = orderItem.UnitPrice;
            existingItem.SubTotal = orderItem.SubTotal;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? orderId, int? productId)
        {
            if (orderId == null || productId == null)
                return NotFound();

            var item = _context.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.Customer)
                .Include(oi => oi.Product)
                .FirstOrDefault(oi =>
                    oi.OrderId == orderId &&
                    oi.ProductId == productId);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int orderId, int productId)
        {
            var item = _context.OrderItems.Find(orderId, productId);

            if (item != null)
            {
                _context.OrderItems.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadOrders(int? selectedId = null)
        {
            var orders = _context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderId)
                .ToList()
                .Select(o => new
                {
                    o.OrderId,
                    Display = "Order #" + o.OrderId +
                              " - " +
                              o.Customer.FirstName +
                              " " +
                              o.Customer.LastName
                })
                .ToList();

            ViewBag.OrderId = new SelectList(
                orders,
                "OrderId",
                "Display",
                selectedId);
        }

        private void LoadProducts(int? selectedId = null)
        {
            var products = _context.Products
                .OrderBy(p => p.Name)
                .ToList()
                .Select(p => new
                {
                    p.ProductId,
                    Display = p.Name + " - " +
                              p.Price.ToString("0.00")
                })
                .ToList();

            ViewBag.ProductId = new SelectList(
                products,
                "ProductId",
                "Display",
                selectedId);
        }
    }
}