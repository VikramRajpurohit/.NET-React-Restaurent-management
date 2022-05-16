using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Restaurentdemo.Models;
using Restaurentdemo.Services;

namespace Restaurentdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodItemService _foodItm;

        public FoodController(FoodItemService Food)
        {
            _foodItm = Food;
        }

        [HttpGet]
        public async Task<List<FoodItem>> Get() =>
            await _foodItm.GetAsync();

        [HttpGet("{FoodItemId:length(24)}")]
        public async Task<ActionResult<FoodItem>> Get(String FoodItemId)
        {
            var food = await _foodItm.getAsync(FoodItemId);

            if(food is null)
            {
                return NotFound();  
            }

            return food;
        }
                
        [HttpPost]
        public async Task<IActionResult> Post(FoodItem itm)
        {
            var food = await _foodItm.getAsyncName(itm.FoodItemName);
            if (food is null) 
            {
                try
                {
                    itm.FoodItemId = ObjectId.GenerateNewId().ToString();
                    await _foodItm.CreateAsync(itm);
                    return CreatedAtAction(nameof(Get), new { FoodItemId = itm.FoodItemId }, itm);
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return StatusCode(409, $"Food '{itm.FoodItemName}' already Exist");
            }
        }

        [HttpPost("{FoodItemId:length(24)}")]

        public async Task<IActionResult> post(String FoodItemId, FoodItem itm)
        {
            var food = await _foodItm.getAsync(FoodItemId);

            if(food is null)
            {
                return NotFound();
            }

            itm.FoodItemId = FoodItemId;

            await _foodItm.UpdateAsync(FoodItemId, itm);
            
            return NoContent(); 
        }

        [HttpDelete("{FoodItemId:length(24)}")]

        public async Task<IActionResult> Delete(String FoodItemId)
        {
            var food = await _foodItm.getAsync(FoodItemId);
            if (food is null)
            {
                return NotFound();
            }
            await _foodItm.RemoveAsync(FoodItemId);
            return NoContent();
        }
    }
}
