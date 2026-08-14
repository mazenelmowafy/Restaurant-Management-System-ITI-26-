using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    public class OrderItemsController : Controller
    {
        public IActionResult Index()
        {
            using var context = new ApplicationDbContext();
            var items = context.OrderItems
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
            if (orderId == null || productId == null) return NotFound();

            using var context = new ApplicationDbContext();
            var item = context.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.Customer)
                .Include(oi => oi.Product)
                .FirstOrDefault(oi => oi.OrderId == orderId && oi.ProductId == productId);

            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create()
        {
            using var context = new ApplicationDbContext();
            LoadOrders(context);
            LoadProducts(context);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderId,ProductId,Quantity,UnitPrice,SubTotal")] OrderItem orderItem)
        {
            ModelState.Remove(nameof(OrderItem.Order));
            ModelState.Remove(nameof(OrderItem.Product));

            using var context = new ApplicationDbContext();

            if (!context.Orders.Any(o => o.OrderId == orderItem.OrderId))
                ModelState.AddModelError(nameof(OrderItem.OrderId), "Please select a valid order.");

            if (!context.Products.Any(p => p.ProductId == orderItem.ProductId))
                ModelState.AddModelError(nameof(OrderItem.ProductId), "Please select a valid product.");

            bool alreadyExists = context.OrderItems.Any(oi =>
                oi.OrderId == orderItem.OrderId && oi.ProductId == orderItem.ProductId);

            if (alreadyExists)
                ModelState.AddModelError(string.Empty, "This product already exists in this order.");

            if (!ModelState.IsValid)
            {
                LoadOrders(context, orderItem.OrderId);
                LoadProducts(context, orderItem.ProductId);
                return View(orderItem);
            }

            context.OrderItems.Add(orderItem);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? orderId, int? productId)
        {
            if (orderId == null || productId == null) return NotFound();

            using var context = new ApplicationDbContext();
            var item = context.OrderItems.Find(orderId, productId);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int orderId, int productId, [Bind("OrderId,ProductId,Quantity,UnitPrice,SubTotal")] OrderItem orderItem)
        {
            if (orderId != orderItem.OrderId || productId != orderItem.ProductId)
                return NotFound();

            ModelState.Remove(nameof(OrderItem.Order));
            ModelState.Remove(nameof(OrderItem.Product));

            if (!ModelState.IsValid)
                return View(orderItem);

            using var context = new ApplicationDbContext();
            var existingItem = context.OrderItems.Find(orderId, productId);
            if (existingItem == null) return NotFound();

            existingItem.Quantity = orderItem.Quantity;
            existingItem.UnitPrice = orderItem.UnitPrice;
            existingItem.SubTotal = orderItem.SubTotal;

            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? orderId, int? productId)
        {
            if (orderId == null || productId == null) return NotFound();

            using var context = new ApplicationDbContext();
            var item = context.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.Customer)
                .Include(oi => oi.Product)
                .FirstOrDefault(oi => oi.OrderId == orderId && oi.ProductId == productId);

            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int orderId, int productId)
        {
            using var context = new ApplicationDbContext();
            var item = context.OrderItems.Find(orderId, productId);

            if (item != null)
            {
                context.OrderItems.Remove(item);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadOrders(ApplicationDbContext context, int? selectedId = null)
        {
            var orders = context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderId)
                .ToList()
                .Select(o => new
                {
                    o.OrderId,
                    Display = "Order #" + o.OrderId + " - " + o.Customer.FirstName + " " + o.Customer.LastName
                })
                .ToList();

            ViewBag.OrderId = new SelectList(orders, "OrderId", "Display", selectedId);
        }

        private void LoadProducts(ApplicationDbContext context, int? selectedId = null)
        {
            var products = context.Products
                .OrderBy(p => p.Name)
                .ToList()
                .Select(p => new
                {
                    p.ProductId,
                    Display = p.Name + " - " + p.Price.ToString("0.00")
                })
                .ToList();

            ViewBag.ProductId = new SelectList(products, "ProductId", "Display", selectedId);
        }
    }
}
