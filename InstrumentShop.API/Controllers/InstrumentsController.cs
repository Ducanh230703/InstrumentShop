using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentsController : ControllerBase
    {
        private readonly InstrumentService _instrumentService;

        public InstrumentsController(InstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Instrument>> GetInstruments()
        {
            return _instrumentService.GetAllInstruments();
        }

        [HttpGet("{id}")]
        public ActionResult<Instrument> GetInstrument(int id)
        {
            var instrument = _instrumentService.GetInstrumentById(id);
            if (instrument == null)
            {
                return NotFound();
            }
            return instrument;
        }

        [HttpPost]
        public ActionResult<Instrument> PostInstrument(Instrument instrument)
        {
            _instrumentService.AddInstrument(instrument);
            return CreatedAtAction(nameof(GetInstrument), new { id = instrument.InstrumentId }, instrument);
        }

        [HttpPut("{id}")]
        public IActionResult PutInstrument(int id, Instrument instrument)
        {
            if (id != instrument.InstrumentId)
            {
                return BadRequest();
            }
            _instrumentService.UpdateInstrument(instrument);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInstrument(int id)
        {
            _instrumentService.DeleteInstrument(id);
            return NoContent();
        }
    }
}