using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace InstrumentShop.API.Services
{
    public class InstrumentService
    {
        private readonly InstrumentShopDbContext _context;

        public InstrumentService(InstrumentShopDbContext context)
        {
            _context = context;
        }

        public List<Instrument> GetAllInstruments()
        {
            return _context.Instruments.ToList();
        }

        public Instrument GetInstrumentById(int id)
        {
            return _context.Instruments.Find(id);
        }

        public void AddInstrument(Instrument instrument)
        {
            _context.Instruments.Add(instrument);
            _context.SaveChanges();
        }

        public void UpdateInstrument(Instrument instrument)
        {
            _context.Instruments.Update(instrument);
            _context.SaveChanges();
        }

        public void DeleteInstrument(int id)
        {
            var instrument = _context.Instruments.Find(id);
            if (instrument != null)
            {
                _context.Instruments.Remove(instrument);
                _context.SaveChanges();
            }
        }
    }
}