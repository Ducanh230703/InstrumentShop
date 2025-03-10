using InstrumentShop.Shared.Models.DTOs;
using InstrumentShop.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentsController : ControllerBase
    {
        private readonly InstrumentService _instrumentService;
        private readonly ILogger<InstrumentsController> _logger;

        public InstrumentsController(InstrumentService instrumentService, ILogger<InstrumentsController> logger)
        {
            _instrumentService = instrumentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstrumentDto>>> GetInstruments()
        {
            var instruments = await _instrumentService.GetAllInstrumentsAsync();
            return Ok(instruments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstrumentDto>> GetInstrument(int id)
        {
            var instrument = await _instrumentService.GetInstrumentByIdAsync(id);
            if (instrument == null)
            {
                return NotFound();
            }
            return Ok(instrument);
        }

        [HttpPost]
        public async Task<ActionResult<InstrumentDto>> PostInstrument(
            [FromForm] string name,
            [FromForm] string description,
            [FromForm] decimal price,
            [FromForm] int categoryId,
            [FromForm] IFormFile image)
        {
            try
            {
                var instrument = new InstrumentDto
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    CategoryId = categoryId
                };

                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        instrument.ImageData = memoryStream.ToArray();
                    }
                }

                var createdInstrument = await _instrumentService.AddInstrumentAsync(instrument, image);
                return CreatedAtAction(nameof(GetInstrument), new { id = createdInstrument.InstrumentId }, createdInstrument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating instrument.");
                return StatusCode(500, $"An error occurred while creating the instrument: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstrument(int id, [FromForm] InstrumentDto instrument, IFormFile image)
        {
            if (id != instrument.InstrumentId)
            {
                return BadRequest();
            }

            try
            {
                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        instrument.ImageData = memoryStream.ToArray();
                    }
                }
                await _instrumentService.UpdateInstrumentAsync(instrument, image);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating instrument {id}.");
                return StatusCode(500, $"An error occurred while updating the instrument: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstrument(int id)
        {
            try
            {
                await _instrumentService.DeleteInstrumentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting instrument {id}.");
                return StatusCode(500, $"An error occurred while deleting the instrument: {ex.Message}");
            }
        }
    }
}