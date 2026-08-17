using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Admin)
                .ToList();

            return View(products);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = _context.Products
                .Include(p => p.Admin)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            LoadAdmins();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Name,Description,IsAvailable,Price,Category,AdminId")]
            Product product)
        {
            ModelState.Remove(nameof(Product.Admin));

            if (!_context.Admins.Any(a => a.AdminId == product.AdminId))
            {
                ModelState.AddModelError(
                    nameof(Product.AdminId),
                    "Please select a valid admin.");
            }

            if (!ModelState.IsValid)
            {
                LoadAdmins(product.AdminId);
                return View(product);
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            LoadAdmins(product.AdminId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("ProductId,Name,Description,IsAvailable,Price,Category,AdminId")]
            Product product)
        {
            if (id != product.ProductId)
                return NotFound();

            ModelState.Remove(nameof(Product.Admin));

            if (!_context.Admins.Any(a => a.AdminId == product.AdminId))
            {
                ModelState.AddModelError(
                    nameof(Product.AdminId),
                    "Please select a valid admin.");
            }

            if (!ModelState.IsValid)
            {
                LoadAdmins(product.AdminId);
                return View(product);
            }

            var existingProduct = _context.Products.Find(id);

            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.IsAvailable = product.IsAvailable;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.AdminId = product.AdminId;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = _context.Products
                .Include(p => p.Admin)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadAdmins(int? selectedId = null)
        {
            var admins = _context.Admins
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => new
                {
                    a.AdminId,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewBag.AdminId = new SelectList(
                admins,
                "AdminId",
                "FullName",
                selectedId);
        }
    }
}