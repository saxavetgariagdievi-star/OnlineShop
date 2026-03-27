using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Response;

namespace OnlineShoppingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost("{userId}/create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder(int userId)
        {
            try
            {
                var result = await _service.CreateOrderAsync(userId);
                if (!result)
                    return BadRequest(ApiResponseExtions.Fail("Cart is empty or user not found"));

                return Ok(ApiResponseExtions.Succsess(result, "Order created"));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            try
            {
                var orders = await _service.GetUserOrdersAsync(userId);
                return Ok(ApiResponseExtions.Succsess(orders));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }

        [HttpPut("{orderId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromQuery] string status)
        {
            try
            {
                var result = await _service.UpdateOrderStatusAsync(orderId, status);
                if (!result)
                    return BadRequest(ApiResponseExtions.Fail("Invalid status or order not found"));

                return Ok(ApiResponseExtions.Succsess(null, "Order status updated"));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }
    }
}