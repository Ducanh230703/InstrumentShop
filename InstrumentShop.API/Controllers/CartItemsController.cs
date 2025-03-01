using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemsController : ControllerBase
    {
        private readonly CartItemService _cartItemService;

        public CartItemsController(CartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet("cart/{cartId}")]
        public ActionResult<IEnumerable<CartItem>> GetCartItemsByCartId(int cartId)
        {
            return _cartItemService.GetCartItemsByCartId(cartId);
        }

        [HttpGet("{id}")]
        public ActionResult<CartItem> GetCartItem(int id)
        {
            var cartItem = _cartItemService.GetCartItemById(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return cartItem;
        }

        [HttpPost]
        public ActionResult<CartItem> PostCartItem(CartItem cartItem)
        {
            _cartItemService.AddCartItem(cartItem);
            return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.CartItemId }, cartItem);
        }

        [HttpPut("{id}")]
        public IActionResult PutCartItem(int id, CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
            {
                return BadRequest();
            }
            _cartItemService.UpdateCartItem(cartItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCartItem(int id)
        {
            _cartItemService.DeleteCartItem(id);
            return NoContent();
        }
    }
}