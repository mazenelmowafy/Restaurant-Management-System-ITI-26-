using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity; // مكتبة التشفير
using RestaurantManagementSystem.Data; // مكتبة قاعدة البيانات (تأكد من مطابقة مسارها لمشروعك)
using RestaurantManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        // 1. تعريف المتغير الخاص بقاعدة البيانات
        private readonly ApplicationDbContext _context;

        // 2. ربط قاعدة البيانات بالكنترولر (Dependency Injection)
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

      
    }
}