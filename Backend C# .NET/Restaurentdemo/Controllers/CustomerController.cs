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

        //
        // Summary: This is for update by id but here no need for customer updation
        //
        //[HttpPost("{customerId:length(24)}")]

        //public async Task<IActionResult> Update( String customerId, Customer updatedcustomer )
        //{
        //    var customer = await _customer.GetAsync(customerId);

        //    if(customer == null)
        //    {
        //        return NotFound();
        //    }

        //    updatedcustomer.customerId = customerId;

        //    await _customer.UpdateAsync(customerId,updatedcustomer);
        //    return NoContent();
        //}

        //
        // Summary: (for Practice)  This is for delete by Id or delete by Name but here no need for customer updation
        //
        //[HttpDelete("{customerId:length(24)}")]
        //public async Task<IActionResult> Delete(String customerId)
        //{
        //    var customer = await _customer.GetAsync(customerId);

        //    if (customer is null)
        //    {
        //        return NotFound();
        //    }

        //    await _customer.RemoveAsync(customer.customerId);
        //    return NoContent();
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteName(String customerName)
        //{
        //    var customer = await _customer.GetAsyncName(customerName);

        //    if(customer is null)
        //    {
        //        return NotFound();
        //    }

        //    await _customer.RemoveAsync(customer.customerId);
        //    return NoContent();
        //}


    }
}
