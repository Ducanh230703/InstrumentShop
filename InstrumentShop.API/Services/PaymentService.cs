using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace InstrumentShop.API.Services
{
    public class PaymentService
    {
        private readonly InstrumentShopDbContext _context;

        public PaymentService(InstrumentShopDbContext context)
        {
            _context = context;
        }

        public Payment GetPaymentByOrderId(int orderId)
        {
            return _context.Payments.FirstOrDefault(p => p.OrderId == orderId);
        }

        public Payment GetPaymentById(int id)
        {
            return _context.Payments.Find(id);
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            _context.SaveChanges();
        }

        public void DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }
    }
}