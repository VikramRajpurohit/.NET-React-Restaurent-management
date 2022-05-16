using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Restaurentdemo.Models;
using Restaurentdemo.Services;

namespace Restaurentdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailService _order;
        private readonly FoodItemService _foodItemService;

        public OrderDetailController(OrderDetailService order, FoodItemService foodItemService)
        {
            _order = order;
            _foodItemService = foodItemService;
        }

        [HttpGet]
        public async Task<List<OrderDetail>> Get() =>
            await _order.GetAsync();

        [HttpGet("{orderId:length(24)}")]
        public async Task<ActionResult<OrderDetailResponse>> Get(String orderId)
        {
            OrderDetailResponse response = new OrderDetailResponse();
            OrderDetail detail = await _order.GetAsync(orderId);
            if (detail == null)
            {
                NotFound();
            }
            else
            {
                response.OrderId = detail.OrderId;
                response.FoodOrderItems = new List<FoodItem>();
                foreach (FoodOrderItem i in detail.FoodOrderItems)
                {
                    FoodItem item = await _foodItemService.getAsync(i.FoodItemId);
                    if (item != null)
                    {
                        response.FoodOrderItems.Add(new FoodItem
                        {
                            FoodItemId = item.FoodItemId,
                            FoodItemName = item.FoodItemName,
                            IsAvailable = item.IsAvailable,
                            Price = item.Price,
                            Veg = item.Veg,
                            Quantity = i.Quantity
                        });
                    }
                }

            }
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderDetail order)
        {
            try
            {
                order.OrderId = ObjectId.GenerateNewId().ToString();
                await _order.CreateAsync(order);
                return CreatedAtAction(nameof(Get), new {OrderID = order.OrderId},order);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
