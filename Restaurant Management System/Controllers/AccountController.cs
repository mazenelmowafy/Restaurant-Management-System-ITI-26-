using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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


        // =========================================
        // PROFILE
        // =========================================

        [Authorize(Roles = "Customer")]
        public IActionResult Profile()
        {
            int customerId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var customer = _context.Customers
                .FirstOrDefault(c => c.CustomerID == customerId);

            if (customer == null)
                return NotFound();

            return View(customer);
        }


        // =========================================
        // LOGIN - GET
        // =========================================

        public IActionResult Login()
        {
            return View();
        }


        // =========================================
        // LOGIN - POST
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string email = model.Email.Trim().ToLower();


            // =========================================
            // CHECK CUSTOMER
            // =========================================

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


            // =========================================
            // CHECK ADMIN
            // =========================================

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


            // =========================================
            // INVALID LOGIN
            // =========================================

            ModelState.AddModelError(
                "",
                "Invalid email or password"
            );

            return View(model);
        }


        // =========================================
        // REGISTER - GET
        // =========================================

        public IActionResult Register()
        {
            return View();
        }


        // =========================================
        // REGISTER - POST
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            model.Email = model.Email.Trim().ToLower();


            bool emailExists = _context.Customers
                .Any(c => c.Email == model.Email);

            if (emailExists)
            {
                ModelState.AddModelError(
                    "Email",
                    "Email already exists"
                );

                return View(model);
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


            // =========================================
            // CREATE CUSTOMER CLAIMS
            // =========================================

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


        // =========================================
        // EDIT PROFILE - GET
        // =========================================

        [Authorize(Roles = "Customer")]
        public IActionResult EditProfile()
        {
            int customerId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));

            var customer = _context.Customers
                .FirstOrDefault(c => c.CustomerID == customerId);

            if (customer == null)
                return NotFound();


            var model = new EditProfileViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Street = customer.Street,
                City = customer.City,
                ZipCode = customer.ZipCode
            };


            return View(model);
        }


        // =========================================
        // EDIT PROFILE - POST
        // =========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> EditProfile(
            EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            int customerId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier));


            var customer = _context.Customers
                .FirstOrDefault(c => c.CustomerID == customerId);

            if (customer == null)
                return NotFound();


            string email = model.Email.Trim().ToLower();


            // =========================================
            // CHECK EMAIL DUPLICATION
            // =========================================

            bool emailExists = _context.Customers
                .Any(c =>
                    c.Email.ToLower() == email &&
                    c.CustomerID != customerId);

            if (emailExists)
            {
                ModelState.AddModelError(
                    "Email",
                    "This email is already used by another account."
                );

                return View(model);
            }


            // =========================================
            // UPDATE CUSTOMER
            // =========================================

            customer.FirstName = model.FirstName.Trim();
            customer.LastName = model.LastName.Trim();
            customer.Email = email;
            customer.Phone = model.Phone.Trim();
            customer.Street = model.Street.Trim();
            customer.City = model.City.Trim();
            customer.ZipCode = model.ZipCode.Trim();


            _context.SaveChanges();


            // =========================================
            // UPDATE CLAIMS
            // =========================================

            var identity = User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var nameClaim = identity.FindFirst(
                    ClaimTypes.Name);

                var emailClaim = identity.FindFirst(
                    ClaimTypes.Email);


                if (nameClaim != null)
                    identity.RemoveClaim(nameClaim);


                if (emailClaim != null)
                    identity.RemoveClaim(emailClaim);


                identity.AddClaim(
                    new Claim(
                        ClaimTypes.Name,
                        customer.FirstName + " " + customer.LastName
                    )
                );


                identity.AddClaim(
                    new Claim(
                        ClaimTypes.Email,
                        customer.Email
                    )
                );
            }


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );


            TempData["ProfileSuccess"] =
                "Your profile has been updated successfully.";


            return RedirectToAction(
                nameof(Profile)
            );
        }


        // =========================================
        // LOGOUT
        // =========================================

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