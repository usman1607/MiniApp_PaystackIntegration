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
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /*[HttpPost("checkout")]
        public IActionResult Checkout([FromBody] CreateOrderRequestModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_orderService.CheckOut(model));
            }

            return NoContent();
        }*/

        [HttpGet("{reference}")]
        public async Task<IActionResult> GetByReference([FromRoute] string reference)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _orderService.GetByReference(reference));
            }

            return NoContent();
        }
    }
}
