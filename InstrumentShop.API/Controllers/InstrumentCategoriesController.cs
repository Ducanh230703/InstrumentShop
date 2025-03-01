using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentCategoriesController : ControllerBase
    {
        private readonly InstrumentCategoryService _instrumentCategoryService;

        public InstrumentCategoriesController(InstrumentCategoryService instrumentCategoryService)
        {
            _instrumentCategoryService = instrumentCategoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstrumentCategory>> GetInstrumentCategories()
        {
            return _instrumentCategoryService.GetAllInstrumentCategories();
        }

        [HttpGet("{id}")]
        public ActionResult<InstrumentCategory> GetInstrumentCategory(int id)
        {
            var category = _instrumentCategoryService.GetInstrumentCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public ActionResult<InstrumentCategory> PostInstrumentCategory(InstrumentCategory category)
        {
            _instrumentCategoryService.AddInstrumentCategory(category);
            return CreatedAtAction(nameof(GetInstrumentCategory), new { id = category.InstrumentCategoryId }, category);
        }

        [HttpPut("{id}")]
        public IActionResult PutInstrumentCategory(int id, InstrumentCategory category)
        {
            if (id != category.InstrumentCategoryId)
            {
                return BadRequest();
            }
            _instrumentCategoryService.UpdateInstrumentCategory(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInstrumentCategory(int id)
        {
            _instrumentCategoryService.DeleteInstrumentCategory(id);
            return NoContent();
        }
    }
}