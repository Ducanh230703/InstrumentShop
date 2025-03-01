using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace InstrumentShop.API.Services
{
    public class InstrumentCategoryService
    {
        private readonly InstrumentShopDbContext _context;

        public InstrumentCategoryService(InstrumentShopDbContext context)
        {
            _context = context;
        }

        public List<InstrumentCategory> GetAllInstrumentCategories()
        {
            return _context.InstrumentCategories.ToList();
        }

        public InstrumentCategory GetInstrumentCategoryById(int id)
        {
            return _context.InstrumentCategories.Find(id);
        }

        public void AddInstrumentCategory(InstrumentCategory category)
        {
            _context.InstrumentCategories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateInstrumentCategory(InstrumentCategory category)
        {
            _context.InstrumentCategories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteInstrumentCategory(int id)
        {
            var category = _context.InstrumentCategories.Find(id);
            if (category != null)
            {
                _context.InstrumentCategories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}