using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            using var context = new ApplicationDbContext();
            var products = context.Products.Include(p => p.Admin).ToList();
            return View(products);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            using var context = new ApplicationDbContext();
            var product = context.Products
                .Include(p => p.Admin)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null) return NotFound();
            return View(product);
        }

        public IActionResult Create()
        {
            using var context = new ApplicationDbContext();
            LoadAdmins(context);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,IsAvailable,Price,Category,AdminId")] Product product)
        {
            ModelState.Remove(nameof(Product.Admin));

            using var context = new ApplicationDbContext();

            if (!context.Admins.Any(a => a.AdminId == product.AdminId))
                ModelState.AddModelError(nameof(Product.AdminId), "Please select a valid admin.");

            if (!ModelState.IsValid)
            {
                LoadAdmins(context, product.AdminId);
                return View(product);
            }

            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            using var context = new ApplicationDbContext();
            var product = context.Products.Find(id);
            if (product == null) return NotFound();

            LoadAdmins(context, product.AdminId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,Name,Description,IsAvailable,Price,Category,AdminId")] Product product)
        {
            if (id != product.ProductId) return NotFound();

            ModelState.Remove(nameof(Product.Admin));

            using var context = new ApplicationDbContext();

            if (!context.Admins.Any(a => a.AdminId == product.AdminId))
                ModelState.AddModelError(nameof(Product.AdminId), "Please select a valid admin.");

            if (!ModelState.IsValid)
            {
                LoadAdmins(context, product.AdminId);
                return View(product);
            }

            var existingProduct = context.Products.Find(id);
            if (existingProduct == null) return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.IsAvailable = product.IsAvailable;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.AdminId = product.AdminId;

            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            using var context = new ApplicationDbContext();
            var product = context.Products
                .Include(p => p.Admin)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using var context = new ApplicationDbContext();
            var product = context.Products.Find(id);

            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadAdmins(ApplicationDbContext context, int? selectedId = null)
        {
            var admins = context.Admins
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => new
                {
                    a.AdminId,
                    FullName = a.FirstName + " " + a.LastName
                })
                .ToList();

            ViewBag.AdminId = new SelectList(admins, "AdminId", "FullName", selectedId);
        }
    }
}
