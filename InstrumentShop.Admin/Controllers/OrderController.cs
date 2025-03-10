using Microsoft.AspNetCore.Mvc;

namespace InstrumentShop.Admin.Controllers
{
    public class OrderController: Controller
    {
        public IActionResult Order()
        {
            return View();
        }
    }
}
