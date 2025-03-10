using Microsoft.AspNetCore.Mvc;

namespace InstrumentShop.Admin.Controllers
{
    public class InstrumentCategoryController : Controller
    {
        public IActionResult InstrumentCategory()
        {
            return View();
        }
    }
}
