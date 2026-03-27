using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Response;

namespace OnlineShoppingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserCart(int userId)
        {
            try
            {
                var cart = await _service.GetUserCartAsync(userId);
                return Ok(ApiResponseExtions.Succsess(cart));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }

        [HttpPost("{userId}/add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToCart(int userId, [FromQuery] int productId, [FromQuery] int quantity)
        {
            try
            {
                var cart = await _service.AddToCartAsync(userId, productId, quantity);

                if (cart == null)
                    return BadRequest(ApiResponseExtions.Fail("Cannot add to cart"));

                return Ok(ApiResponseExtions.Succsess(cart, "Product added to cart"));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }

        [HttpDelete("{userId}/remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveFromCart(int userId, [FromQuery] int productId)
        {
            try
            {
                var result = await _service.RemoveFromCartAsync(userId, productId);

                if (!result)
                    return NotFound(ApiResponseExtions.Fail("Product not found in cart"));

                return Ok(ApiResponseExtions.Succsess(null, "Product removed from cart"));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }

        [HttpDelete("{userId}/clear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClearCart(int userId)
        {
            try
            {
                var result = await _service.ClearCartAsync(userId);

                if (!result)
                    return NotFound(ApiResponseExtions.Fail("Cart not found"));

                return Ok(ApiResponseExtions.Succsess(null, "Cart cleared"));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }
    }
}