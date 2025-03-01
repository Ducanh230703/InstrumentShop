using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentsController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("order/{orderId}")]
        public ActionResult<Payment> GetPaymentByOrderId(int orderId)
        {
            var payment = _paymentService.GetPaymentByOrderId(orderId);
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> GetPayment(int id)
        {
            var payment = _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }

        [HttpPost]
        public ActionResult<Payment> PostPayment(Payment payment)
        {
            _paymentService.AddPayment(payment);
            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }

        [HttpPut("{id}")]
        public IActionResult PutPayment(int id, Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return BadRequest();
            }
            _paymentService.UpdatePayment(payment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            _paymentService.DeletePayment(id);
            return NoContent();
        }
    }
}