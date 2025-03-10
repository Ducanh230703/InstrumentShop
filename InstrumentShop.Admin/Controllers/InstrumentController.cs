using Microsoft.AspNetCore.Mvc;

namespace InstrumentShop.Admin.Controllers
{
    public class InstrumentController : Controller
    {
        public IActionResult Instrument()
        {
            return View();
        }
    }
}
