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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CreateOrderRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _paymentService.CheckOut(model);
                return Ok(response);
            }

            return NoContent();
        }

        [HttpGet("{reference}")]
        public async Task<IActionResult> VerifyPayment([FromRoute] string reference)
        {
            return Ok(await _paymentService.VerifyPayment(reference));
        }
    }
}
