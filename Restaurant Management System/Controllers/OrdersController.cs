using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            using var context = new ApplicationDbContext();
            var orders = context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            using var context = new ApplicationDbContext();
            var order = context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();
            return View(order);
        }

        public IActionResult Create()
        {
            using var context = new ApplicationDbContext();
            LoadCustomers(context);

            return View(new Order
            {
                OrderDate = DateTime.Now,
                Status = "Pending"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderDate,Status,TotalAmount,CustomerId")] Order order)
        {
            ModelState.Remove(nameof(Order.Customer));
            ModelState.Remove(nameof(Order.OrderItems));

            using var context = new ApplicationDbContext();

            if (!context.Customers.Any(c => c.CustomerID == order.CustomerId))
                ModelState.AddModelError(nameof(Order.CustomerId), "Please select a valid customer.");

            if (!ModelState.IsValid)
            {
                LoadCustomers(context, order.CustomerId);
                return View(order);
            }

            context.Orders.Add(order);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            using var context = new ApplicationDbContext();
            var order = context.Orders.Find(id);
            if (order == null) return NotFound();

            LoadCustomers(context, order.CustomerId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("OrderId,OrderDate,Status,TotalAmount,CustomerId")] Order order)
        {
            if (id != order.OrderId) return NotFound();

            ModelState.Remove(nameof(Order.Customer));
            ModelState.Remove(nameof(Order.OrderItems));

            using var context = new ApplicationDbContext();

            if (!context.Customers.Any(c => c.CustomerID == order.CustomerId))
                ModelState.AddModelError(nameof(Order.CustomerId), "Please select a valid customer.");

            if (!ModelState.IsValid)
            {
                LoadCustomers(context, order.CustomerId);
                return View(order);
            }

            var existingOrder = context.Orders.Find(id);
            if (existingOrder == null) return NotFound();

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.CustomerId = order.CustomerId;

            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            using var context = new ApplicationDbContext();
            var order = context.Orders
                .Include(o => o.Customer)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using var context = new ApplicationDbContext();
            var order = context.Orders.Find(id);

            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadCustomers(ApplicationDbContext context, int? selectedId = null)
        {
            var customers = context.Customers
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .Select(c => new
                {
                    c.CustomerID,
                    FullName = c.FirstName + " " + c.LastName
                })
                .ToList();

            ViewBag.CustomerId = new SelectList(customers, "CustomerID", "FullName", selectedId);
        }
    }
}
