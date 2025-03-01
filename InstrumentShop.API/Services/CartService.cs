using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace InstrumentShop.API.Services
{
    public class CartService
    {
        private readonly InstrumentShopDbContext _context;

        public CartService(InstrumentShopDbContext context)
        {
            _context = context;
        }

        public Cart GetCartByCustomerId(int customerId)
        {
            return _context.Carts.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public void AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }

        public void DeleteCart(int cartId)
        {
            var cart = _context.Carts.Find(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
        }
    }
}