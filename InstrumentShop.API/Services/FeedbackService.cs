using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace InstrumentShop.API.Services
{
    public class FeedbackService
    {
        private readonly InstrumentShopDbContext _context;

        public FeedbackService(InstrumentShopDbContext context)
        {
            _context = context;
        }

        public List<Feedback> GetAllFeedbacks()
        {
            return _context.Feedbacks.ToList();
        }

        public Feedback GetFeedbackById(int id)
        {
            return _context.Feedbacks.Find(id);
        }

        public void AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public void UpdateFeedback(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            _context.SaveChanges();
        }

        public void DeleteFeedback(int id)
        {
            var feedback = _context.Feedbacks.Find(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                _context.SaveChanges();
            }
        }
    }
}