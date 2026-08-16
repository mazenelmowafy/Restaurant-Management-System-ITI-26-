using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        // Needed to find the wwwroot folder path so we can save uploaded images there.
        // This is the one thing that can't be done with "new ApplicationDbContext()" style,
        // so it comes in through the constructor.
        private readonly IWebHostEnvironment _environment;

        // Folder (inside wwwroot) where product images are saved.
        private const string ImagesFolder = "images/products";

        public ProductsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

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
        public IActionResult Create(
            [Bind("Name,Description,IsAvailable,Price,Category,AdminId")] Product product,
            IFormFile? ImageFile)
        {
            ModelState.Remove(nameof(Product.Admin));

            using var context = new ApplicationDbContext();

            if (!context.Admins.Any(a => a.AdminId == product.AdminId))
                ModelState.AddModelError(nameof(Product.AdminId), "Please select a valid admin.");

            if (ImageFile != null && !IsValidImage(ImageFile))
                ModelState.AddModelError(nameof(ImageFile), "Please upload a valid image file (jpg, jpeg, png, gif) under 5 MB.");

            if (!ModelState.IsValid)
            {
                LoadAdmins(context, product.AdminId);
                return View(product);
            }

            if (ImageFile != null)
            {
                product.ImageFileName = SaveImage(ImageFile);
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
        public IActionResult Edit(
            int id,
            [Bind("ProductId,Name,Description,IsAvailable,Price,Category,AdminId,ImageFileName")] Product product,
            IFormFile? ImageFile,
            bool RemoveImage = false)
        {
            if (id != product.ProductId) return NotFound();

            ModelState.Remove(nameof(Product.Admin));

            using var context = new ApplicationDbContext();

            if (!context.Admins.Any(a => a.AdminId == product.AdminId))
                ModelState.AddModelError(nameof(Product.AdminId), "Please select a valid admin.");

            if (ImageFile != null && !IsValidImage(ImageFile))
                ModelState.AddModelError(nameof(ImageFile), "Please upload a valid image file (jpg, jpeg, png, gif) under 5 MB.");

            if (!ModelState.IsValid)
            {
                LoadAdmins(context, product.AdminId);
                return View(product);
            }

            var existingProduct = context.Products.Find(id);
            if (existingProduct == null) return NotFound();

            var oldImageFileName = existingProduct.ImageFileName;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.IsAvailable = product.IsAvailable;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.AdminId = product.AdminId;

            if (ImageFile != null)
            {
                existingProduct.ImageFileName = SaveImage(ImageFile);
            }
            else if (RemoveImage)
            {
                existingProduct.ImageFileName = null;
            }

            context.SaveChanges();

            if ((ImageFile != null || RemoveImage) && !string.IsNullOrEmpty(oldImageFileName))
            {
                DeleteImageFile(oldImageFileName);
            }

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

                if (!string.IsNullOrEmpty(product.ImageFileName))
                {
                    DeleteImageFile(product.ImageFileName);
                }
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

        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private const long MaxImageBytes = 5 * 1024 * 1024; // 5 MB

        private static bool IsValidImage(IFormFile file)
        {
            if (file.Length <= 0 || file.Length > MaxImageBytes) return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return AllowedExtensions.Contains(extension);
        }

        private string SaveImage(IFormFile file)
        {
            var uploadsPath = Path.Combine(_environment.WebRootPath, ImagesFolder);
            Directory.CreateDirectory(uploadsPath); // no-op if it already exists

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return uniqueFileName;
        }

        private void DeleteImageFile(string fileName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, ImagesFolder, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
