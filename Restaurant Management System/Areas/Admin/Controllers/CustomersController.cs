using Microsoft.AspNetCore.Mvc;

namespace Restaurant_Management_System.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
