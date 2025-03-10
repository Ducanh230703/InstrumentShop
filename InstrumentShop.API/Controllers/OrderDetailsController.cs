using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using InstrumentShop.Shared.Models.DTOs;

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
        public ActionResult<IEnumerable<OrderDetailDto>> GetOrderDetailsByOrderId(int orderId)
        {
            return _orderDetailService.GetOrderDetailsByOrderId(orderId);
        }

        [HttpGet("{orderId}/{instrumentId}")]
        public ActionResult<OrderDetailDto> GetOrderDetail(int orderId, int instrumentId)
        {
            var orderDetail = _orderDetailService.GetOrderDetailById(orderId, instrumentId);
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
            return CreatedAtAction(nameof(GetOrderDetail), new { orderId = orderDetail.OrderId, instrumentId = orderDetail.InstrumentId }, orderDetail);
        }

        [HttpPut("{orderId}/{instrumentId}")]
        public IActionResult PutOrderDetail(int orderId, int instrumentId, OrderDetail orderDetail)
        {
            if (orderId != orderDetail.OrderId || instrumentId != orderDetail.InstrumentId)
            {
                return BadRequest();
            }

            _orderDetailService.UpdateOrderDetail(orderDetail);
            return NoContent();
        }

        [HttpDelete("{orderId}/{instrumentId}")]
        public IActionResult DeleteOrderDetail(int orderId, int instrumentId)
        {
            _orderDetailService.DeleteOrderDetail(orderId, instrumentId);
            return NoContent();
        }
    }
}