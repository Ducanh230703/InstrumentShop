using InstrumentShop.Shared.Models.DTOs;
using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InstrumentShop.API.Services
{
    public class InstrumentService
    {
        private readonly InstrumentShopDbContext _context;

        public InstrumentService(InstrumentShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstrumentDto>> GetAllInstrumentsAsync()
        {
            var instruments = await _context.Instruments
                .Include(i => i.Category)
                .Select(i => new InstrumentDto
                {
                    InstrumentId = i.InstrumentId,
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    ImageData = i.ImageData,
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category.CategoryName
                })
                .ToListAsync();
            foreach (var instrument in instruments)
            {
                if (instrument.ImageData != null)
                {
                    Console.WriteLine($"Instrument ID: {instrument.InstrumentId}, ImageData Length: {instrument.ImageData.Length}");
                }
                else
                {
                    Console.WriteLine($"Instrument ID: {instrument.InstrumentId}, ImageData is null");
                }
            }

            return instruments;
        }

        public async Task<InstrumentDto> GetInstrumentByIdAsync(int id)
        {
            var instrument = await _context.Instruments
                .Include(i => i.Category)
                .Where(i => i.InstrumentId == id)
                .Select(i => new InstrumentDto
                {
                    InstrumentId = i.InstrumentId,
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    ImageData = i.ImageData,
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category.CategoryName
                })
                .FirstOrDefaultAsync();

            return instrument;
        }

        public async Task<InstrumentDto> AddInstrumentAsync(InstrumentDto instrumentDto, IFormFile imageFile)
        {
            var instrument = new Instrument
            {
                Name = instrumentDto.Name,
                Description = instrumentDto.Description,
                Price = instrumentDto.Price,
                CategoryId = instrumentDto.CategoryId
            };

            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    instrument.ImageData = memoryStream.ToArray();
                }
            }

            _context.Instruments.Add(instrument);
            await _context.SaveChangesAsync();

            instrumentDto.InstrumentId = instrument.InstrumentId;
            instrumentDto.CategoryName = _context.InstrumentCategories.Find(instrument.CategoryId)?.CategoryName;

            return instrumentDto;
        }

        public async Task UpdateInstrumentAsync(InstrumentDto instrumentDto, IFormFile imageFile)
        {
            var instrument = await _context.Instruments.FindAsync(instrumentDto.InstrumentId);
            if (instrument == null)
            {
                return; // Hoặc throw exception nếu cần
            }

            instrument.Name = instrumentDto.Name;
            instrument.Description = instrumentDto.Description;
            instrument.Price = instrumentDto.Price;
            instrument.CategoryId = instrumentDto.CategoryId;

            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    instrument.ImageData = memoryStream.ToArray();
                }
            }

            _context.Instruments.Update(instrument);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInstrumentAsync(int id)
        {
            var instrument = await _context.Instruments.FindAsync(id);
            if (instrument != null)
            {
                _context.Instruments.Remove(instrument);
                await _context.SaveChangesAsync();
            }
        }
    }
}