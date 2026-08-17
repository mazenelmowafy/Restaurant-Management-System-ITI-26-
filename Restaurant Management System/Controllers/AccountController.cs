using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.ViewModels;

namespace RestaurantManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string email = model.Email.Trim().ToLower();

            var customer = _context.Customers
                .FirstOrDefault(c => c.Email.ToLower() == email);

            if (customer == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password"
                );

                return View(model);
            }


            var passwordHasher =
                new PasswordHasher<Customer>();

            var result = passwordHasher.VerifyHashedPassword(
                customer,
                customer.Password,
                model.Password
            );


            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password"
                );

                return View(model);
            }


            HttpContext.Session.SetInt32(
                "CustomerId",
                customer.CustomerID
            );

            HttpContext.Session.SetString(
                "CustomerName",
                customer.FirstName + " " + customer.LastName
            );


            return RedirectToAction(
                "Index",
                "Products"
            );
        }



        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);


            customer.Email =
                customer.Email.Trim().ToLower();


            bool emailExists = _context.Customers
                .Any(c => c.Email == customer.Email);

            if (emailExists)
            {
                ModelState.AddModelError(
                    "Email",
                    "Email already exists"
                );

                return View(customer);
            }


            var passwordHasher =
                new PasswordHasher<Customer>();

            customer.Password =
                passwordHasher.HashPassword(
                    customer,
                    customer.Password
                );


            _context.Customers.Add(customer);
            _context.SaveChanges();


            HttpContext.Session.SetInt32(
                "CustomerId",
                customer.CustomerID
            );

            HttpContext.Session.SetString(
                "CustomerName",
                customer.FirstName + " " + customer.LastName
            );


            return RedirectToAction(
                "Index",
                "Products"
            );
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Login",
                "Account"
            );
        }
    }
}