using Microsoft.AspNetCore.Mvc;
using Restaurentdemo.Models;
using System.Linq;

using Restaurentdemo.Services;
using MongoDB.Bson;

namespace Restaurentdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customer;

        public CustomerController(CustomerService customer)
        {
            _customer = customer;
        }

        [HttpGet]
        public async Task<List<Customer>> Get() =>
        
             await _customer.GetAsync();

        [HttpGet("{customerId:length(24)}")]
        public async Task<ActionResult<Customer>> Get(String customerId)
        {
            var customer = await _customer.GetAsync(customerId);

            if(customer is null)
            {
                return NotFound();
            }
            return customer;
        }

        

        [HttpPost]
        public async Task<IActionResult> Post( Customer customer)
        {
            try
            {
                customer.customerId = ObjectId.GenerateNewId().ToString();
                await _customer.CreateAsync(customer);
                return CreatedAtAction(nameof(Get), new {customerId = customer.customerId }, customer);


            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{customerId:length(24)}")]

        public async Task<IActionResult> Update( String customerId, Customer updatedcustomer )
        {
            var customer = await _customer.GetAsync(customerId);

            if(customer == null)
            {
                return NotFound();
            }

            updatedcustomer.customerId = customerId;

            await _customer.UpdateAsync(customerId,updatedcustomer);
            return NoContent();
            
        }
        
    }   
}
