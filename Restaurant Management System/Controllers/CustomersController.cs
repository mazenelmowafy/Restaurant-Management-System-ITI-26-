using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            using var context = new ApplicationDbContext();

            var customers = context.Customers.ToList();

            return View(customers);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            using var context = new ApplicationDbContext();
            var customer = context.Customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Email,Phone,Street,City,ZipCode")] Customer customer)
        {
            ModelState.Remove(nameof(Customer.Orders));

            if (!ModelState.IsValid)
                return View(customer);

            using var context = new ApplicationDbContext();
            context.Customers.Add(customer);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            using var context = new ApplicationDbContext();
            var customer = context.Customers.Find(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CustomerID,FirstName,LastName,Email,Phone,Street,City,ZipCode")] Customer customer)
        {
            if (id != customer.CustomerID) return NotFound();

            ModelState.Remove(nameof(Customer.Orders));

            if (!ModelState.IsValid)
                return View(customer);

            using var context = new ApplicationDbContext();
            var existingCustomer = context.Customers.Find(id);
            if (existingCustomer == null) return NotFound();

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Street = customer.Street;
            existingCustomer.City = customer.City;
            existingCustomer.ZipCode = customer.ZipCode;

            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            using var context = new ApplicationDbContext();
            var customer = context.Customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using var context = new ApplicationDbContext();
            var customer = context.Customers.Find(id);

            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
