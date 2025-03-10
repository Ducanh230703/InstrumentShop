using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return _orderService.GetAllOrders();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            _orderService.AddOrder(order);
            // Tính toán TotalAmount sau khi thêm Order
            _orderService.CalculateTotalAmount(order.OrderId);
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }
            _orderService.UpdateOrder(order);
            // Tính toán TotalAmount sau khi cập nhật Order
            _orderService.CalculateTotalAmount(order.OrderId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}