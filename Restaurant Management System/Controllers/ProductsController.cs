using Microsoft.AspNetCore.Authorization; // تمت الإضافة
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;
using System.Security.Claims; // تمت الإضافة

namespace RestaurantManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // مفتوحة للجميع يشوفوا المنيو
        public IActionResult Index(string? category)
        {
            var products = _context.Products
                .Where(p => p.IsAvailable);

            if (!string.IsNullOrEmpty(category))
            {
                products = products
                    .Where(p => p.Category == category);
            }

            return View(products.ToList());
        }

        // مفتوحة للجميع يشوفوا التفاصيل
        public IActionResult Details(int id)
        {
            var product = _context.Products
                .FirstOrDefault(p =>
                    p.ProductId == id &&
                    p.IsAvailable);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // محمية: لازم يكون مسجل دخول كـ Customer عشان يضيف للسلة
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            // استخراج الـ ID الخاص بالعميل من الـ Claims بدلاً من السشن
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (quantity <= 0)
                quantity = 1;

            var product = _context.Products
                .FirstOrDefault(p =>
                    p.ProductId == productId &&
                    p.IsAvailable);

            if (product == null)
                return NotFound();

            var cart = _context.Orders
                .FirstOrDefault(o =>
                    o.CustomerId == customerId && // تم التعديل هنا
                    o.Status == "Cart");

            if (cart == null)
            {
                cart = new Order
                {
                    CustomerId = customerId, // تم التعديل هنا
                    OrderDate = DateTime.Now,
                    Status = "Cart",
                    TotalAmount = 0
                };

                _context.Orders.Add(cart);
                _context.SaveChanges();
            }

            var cartItem = _context.OrderItems
                .FirstOrDefault(i =>
                    i.OrderId == cart.OrderId &&
                    i.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new OrderItem
                {
                    OrderId = cart.OrderId,
                    ProductId = product.ProductId,
                    Quantity = quantity,
                    UnitPrice = product.Price,
                    SubTotal = product.Price * quantity
                };

                _context.OrderItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
                cartItem.UnitPrice = product.Price;
                cartItem.SubTotal = cartItem.Quantity * product.Price;
            }

            _context.SaveChanges();

            cart.TotalAmount = _context.OrderItems
                .Where(i => i.OrderId == cart.OrderId)
                .Sum(i => i.SubTotal);

            _context.SaveChanges();

            TempData["CartSuccess"] = "Your item has been added to your cart!";

            return RedirectToAction(nameof(Index));
        }
    }
}