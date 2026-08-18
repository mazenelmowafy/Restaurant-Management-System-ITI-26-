using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Data;

namespace RestaurantManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var admin = _context.Admins.FirstOrDefault();

            if (admin == null)
                return NotFound();

            return View(admin);
        }
    }
}