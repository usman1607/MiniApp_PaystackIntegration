using Microsoft.AspNetCore.Mvc;
using PaystackIntegration.Dtos;
using PaystackIntegration.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaystackIntegration.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("checkout")]
        public IActionResult Checkout([FromBody] CreateOrderRequestModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_paymentService.CheckOut(model));
            }

            return NoContent();
        }

        [HttpGet("{reference}")]
        public IActionResult VerifyPayment([FromRoute] string reference)
        {
            return Ok(_paymentService.VerifyPayment(reference));
        }
    }
}
