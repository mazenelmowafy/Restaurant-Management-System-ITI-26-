using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Customers
        public IActionResult Index()
        {
            var customers = _context.Customers
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToList();

            return View(customers);
        }

        // GET: /Admin/Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var customer = _context.Customers
                .FirstOrDefault(c => c.CustomerID == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // GET: /Admin/Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("FirstName,LastName,Email,Password,Phone,Street,City,ZipCode")]
            Customer customer)
        {
            ModelState.Remove(nameof(Customer.Orders));

            if (!ModelState.IsValid)
                return View(customer);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: /Admin/Customers/Edit/5
        // POST: /Admin/Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("CustomerID,FirstName,LastName,Email,Phone,Street,City,ZipCode")] Customer customer)
        {
            if (id != customer.CustomerID)
                return NotFound();

            ModelState.Remove(nameof(Customer.Orders));

            // Password is optional during Edit
            if (string.IsNullOrWhiteSpace(customer.Password))
            {
                ModelState.Remove(nameof(Customer.Password));
            }

            if (!ModelState.IsValid)
                return View(customer);

            var existingCustomer = _context.Customers.Find(id);

            if (existingCustomer == null)
                return NotFound();

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Street = customer.Street;
            existingCustomer.City = customer.City;
            existingCustomer.ZipCode = customer.ZipCode;

            // Update password only if a new password was entered
            if (!string.IsNullOrWhiteSpace(customer.Password))
            {
                existingCustomer.Password = customer.Password;
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Customers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var customer = _context.Customers
                .FirstOrDefault(c => c.CustomerID == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: /Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}