using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailService _orderDetailService;

        public OrderDetailsController(OrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("order/{orderId}")]
        public ActionResult<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int orderId)
        {
            return _orderDetailService.GetOrderDetailsByOrderId(orderId);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDetail> GetOrderDetail(int id)
        {
            var orderDetail = _orderDetailService.GetOrderDetailById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return orderDetail;
        }

        [HttpPost]
        public ActionResult<OrderDetail> PostOrderDetail(OrderDetail orderDetail)
        {
            _orderDetailService.AddOrderDetail(orderDetail);
            return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        [HttpPut("{id}")]
        public IActionResult PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return BadRequest();
            }
            _orderDetailService.UpdateOrderDetail(orderDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            _orderDetailService.DeleteOrderDetail(id);
            return NoContent();
        }
    }
}