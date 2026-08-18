using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HelpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}