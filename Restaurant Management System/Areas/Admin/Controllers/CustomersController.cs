using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // تمت إضافة هذه المكتبة للتشفير
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
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToListAsync();

            return View(customers);
        }

        // GET: /Admin/Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerID == id);

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
        public async Task<IActionResult> Create(
            [Bind("FirstName,LastName,Email,PasswordHash,Phone,Street,City,ZipCode")] Customer customer) // تم تعديل Password إلى PasswordHash
        {
            ModelState.Remove(nameof(Customer.Orders));

            if (!ModelState.IsValid)
                return View(customer);

            // تشفير كلمة المرور قبل الحفظ
            if (!string.IsNullOrWhiteSpace(customer.PasswordHash))
            {
                var passwordHasher = new PasswordHasher<Customer>();
                customer.PasswordHash = passwordHasher.HashPassword(customer, customer.PasswordHash);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: /Admin/Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("CustomerID,FirstName,LastName,Email,PasswordHash,Phone,Street,City,ZipCode")] Customer customer) // تم تعديل Password إلى PasswordHash
        {
            if (id != customer.CustomerID)
                return NotFound();

            ModelState.Remove(nameof(Customer.Orders));

            // جعل كلمة المرور اختيارية عند التعديل
            if (string.IsNullOrWhiteSpace(customer.PasswordHash))
            {
                ModelState.Remove(nameof(Customer.PasswordHash));
            }

            if (!ModelState.IsValid)
                return View(customer);

            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
                return NotFound();

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Street = customer.Street;
            existingCustomer.City = customer.City;
            existingCustomer.ZipCode = customer.ZipCode;

            // تحديث وتشفير كلمة المرور فقط إذا تم إدخال كلمة مرور جديدة
            if (!string.IsNullOrWhiteSpace(customer.PasswordHash))
            {
                var passwordHasher = new PasswordHasher<Customer>();
                existingCustomer.PasswordHash = passwordHasher.HashPassword(existingCustomer, customer.PasswordHash);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CustomerID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var customer = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerID == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // POST: /Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}