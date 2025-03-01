using InstrumentShop.Shared.Data;
using InstrumentShop.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace InstrumentShop.API.Services
{
    public class CartItemService
    {
        private readonly InstrumentShopDbContext _context;

        public CartItemService(InstrumentShopDbContext context)
        {
            _context = context;
        }

        public List<CartItem> GetCartItemsByCartId(int cartId)
        {
            return _context.CartItems.Where(ci => ci.CartId == cartId).ToList();
        }

        public CartItem GetCartItemById(int id)
        {
            return _context.CartItems.Find(id);
        }

        public void AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            _context.SaveChanges();
        }

        public void DeleteCartItem(int id)
        {
            var cartItem = _context.CartItems.Find(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }
    }
}