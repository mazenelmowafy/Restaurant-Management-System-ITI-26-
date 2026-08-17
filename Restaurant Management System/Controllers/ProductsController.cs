using Microsoft.AspNetCore.Mvc;
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
                .Where(p => p.IsAvailable)
                .ToList();

            return View(products);
        }

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {

            var customerId =
                HttpContext.Session.GetInt32("CustomerId");

            if (customerId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }


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
                    o.CustomerId == customerId.Value &&
                    o.Status == "Cart");


            if (cart == null)
            {
                cart = new Order
                {
                    CustomerId = customerId.Value,
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

                cartItem.SubTotal =
                    cartItem.Quantity * product.Price;
            }


            _context.SaveChanges();


            cart.TotalAmount = _context.OrderItems
                .Where(i => i.OrderId == cart.OrderId)
                .Sum(i => i.SubTotal);

            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
    }
}