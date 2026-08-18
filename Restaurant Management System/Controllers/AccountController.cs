using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Management_System.ViewModels;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.ViewModels;
using System.Security.Claims;

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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string email = model.Email.Trim().ToLower();

            // =========================
            // Check Customer
            // =========================

            var customer = _context.Customers
                .FirstOrDefault(c => c.Email.ToLower() == email);

            if (customer != null)
            {
                var passwordHasher = new PasswordHasher<Customer>();

                var result = passwordHasher.VerifyHashedPassword(
                    customer,
                    customer.PasswordHash,
                    model.Password
                );

                if (result != PasswordVerificationResult.Failed)
                {
                    var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    customer.CustomerID.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    customer.FirstName + " " + customer.LastName
                ),

                new Claim(
                    ClaimTypes.Email,
                    customer.Email
                ),

                new Claim(
                    ClaimTypes.Role,
                    "Customer"
                )
            };

                    var identity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal
                    );

                    return RedirectToAction(
                        "Index",
                        "Products"
                    );
                }
            }


            // =========================
            // Check Admin
            // =========================

            var admin = _context.Admins
                .FirstOrDefault(a => a.Email.ToLower() == email);

            if (admin != null)
            {
                var passwordHasher = new PasswordHasher<Admin>();

                var result = passwordHasher.VerifyHashedPassword(
                    admin,
                    admin.PasswordHash,
                    model.Password
                );

                if (result != PasswordVerificationResult.Failed)
                {
                    var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    admin.AdminId.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    admin.FirstName + " " + admin.LastName
                ),

                new Claim(
                    ClaimTypes.Email,
                    admin.Email
                ),

                new Claim(
                    ClaimTypes.Role,
                    "Admin"
                )
            };

                    var identity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme
                    );

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal
                    );

                    return RedirectToAction(
                        "Index",
                        "Dashboard",
                        new { area = "Admin" }
                    );
                }
            }


            // =========================
            // Invalid Login
            // =========================

            ModelState.AddModelError(
                "",
                "Invalid email or password"
            );

            return View(model);
        }




        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model) // 1. إضافة اسم المتغير 'model'
        {
            if (!ModelState.IsValid)
                return View(model); // 2. استخدام 'model' بدلاً من 'customer'


            model.Email = model.Email.Trim().ToLower(); // 3. استخدام 'model' بدلاً من 'customer'


            bool emailExists = _context.Customers
                .Any(c => c.Email == model.Email); // 4. استخدام 'model' بدلاً من 'customer'

            if (emailExists)
            {
                ModelState.AddModelError(
                    "Email",
                    "Email already exists"
                );

                return View(model); // 5. استخدام 'model' بدلاً من 'customer'
            }


            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Street = model.Street,
                City = model.City,
                ZipCode = model.ZipCode
            };

            var passwordHasher = new PasswordHasher<Customer>();

            customer.PasswordHash = passwordHasher.HashPassword(
                customer,
                model.Password
            );


            _context.Customers.Add(customer);
            _context.SaveChanges();


            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, customer.CustomerID.ToString()),
        new Claim(ClaimTypes.Name, customer.FirstName + " " + customer.LastName),
        new Claim(ClaimTypes.Email, customer.Email),
        new Claim(ClaimTypes.Role, "Customer")
    };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToAction(
                "Index",
                "Products"
            );
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction(
                "Login",
                "Account"
            );
        }
    }
}