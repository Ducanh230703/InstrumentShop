using Microsoft.AspNetCore.Mvc;

namespace InstrumentShop.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}