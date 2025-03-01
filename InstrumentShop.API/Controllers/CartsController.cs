using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartsController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("customer/{customerId}")]
        public ActionResult<Cart> GetCartByCustomerId(int customerId)
        {
            var cart = _cartService.GetCartByCustomerId(customerId);
            if (cart == null)
            {
                return NotFound();
            }
            return cart;
        }

        [HttpPost]
        public ActionResult<Cart> PostCart(Cart cart)
        {
            _cartService.AddCart(cart);
            return CreatedAtAction(nameof(GetCartByCustomerId), new { customerId = cart.CustomerId }, cart);
        }

        [HttpPut("{id}")]
        public IActionResult PutCart(int id, Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest();
            }
            _cartService.UpdateCart(cart);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCart(int id)
        {
            _cartService.DeleteCart(id);
            return NoContent();
        }
    }
}