using Microsoft.AspNetCore.Mvc;

namespace InstrumentShop.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}
