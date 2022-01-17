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
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateCustomerRequestModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_customerService.Register(model));
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            return Ok(_customerService.GetById(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_customerService.GetAll());
        }
    }
}
