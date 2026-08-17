using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

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
            var orders = _context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var order = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        public IActionResult Create()
        {
            LoadCustomers();

            return View(new Order
            {
                OrderDate = DateTime.Now,
                Status = "Pending"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("OrderDate,Status,TotalAmount,CustomerId")]
            Order order)
        {
            ModelState.Remove(nameof(Order.Customer));
            ModelState.Remove(nameof(Order.OrderItems));

            if (!_context.Customers.Any(c => c.CustomerID == order.CustomerId))
            {
                ModelState.AddModelError(
                    nameof(Order.CustomerId),
                    "Please select a valid customer.");
            }

            if (!ModelState.IsValid)
            {
                LoadCustomers(order.CustomerId);
                return View(order);
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            LoadCustomers(order.CustomerId);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("OrderId,OrderDate,Status,TotalAmount,CustomerId")]
            Order order)
        {
            if (id != order.OrderId)
                return NotFound();

            ModelState.Remove(nameof(Order.Customer));
            ModelState.Remove(nameof(Order.OrderItems));

            if (!_context.Customers.Any(c => c.CustomerID == order.CustomerId))
            {
                ModelState.AddModelError(
                    nameof(Order.CustomerId),
                    "Please select a valid customer.");
            }

            if (!ModelState.IsValid)
            {
                LoadCustomers(order.CustomerId);
                return View(order);
            }

            var existingOrder = _context.Orders.Find(id);

            if (existingOrder == null)
                return NotFound();

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.CustomerId = order.CustomerId;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var order = _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _context.Orders.Find(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadCustomers(int? selectedId = null)
        {
            var customers = _context.Customers
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .Select(c => new
                {
                    c.CustomerID,
                    FullName = c.FirstName + " " + c.LastName
                })
                .ToList();

            ViewBag.CustomerId = new SelectList(
                customers,
                "CustomerID",
                "FullName",
                selectedId);
        }
    }
}