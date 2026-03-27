using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Response;

namespace OnlineShoppingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var product = await _service.GetAllAsync();
                return Ok(ApiResponseExtions.Succsess(product));
            }
            catch
            {

                return StatusCode(500, ApiResponseExtions.Fail("Server error occurde"));
            }
        }

        [HttpGet("Product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _service.GetByIdAsync(id);
                if (product == null) return NotFound(ApiResponseExtions.Fail("Product not found"));
                return Ok(ApiResponseExtions.Succsess(product));

            }
            catch
            {

                return StatusCode(500, ApiResponseExtions.Fail("Server error occurde"));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponseExtions.Fail("Validator error"));
                var create = await _service.CreateAsync(dto);
                return Ok(ApiResponseExtions.Succsess(create, "Product validator"));
            }
            catch
            {

                return StatusCode(500, ApiResponseExtions.Fail("Server Error Occurde"));
            }
        }

        [HttpPut("Product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponseExtions.Fail("Validator Eroro"));
                var update = await _service.UpdateAsync(id, dto);
                if (update == null)
                    return NotFound(ApiResponseExtions.Fail("Product Not Found"));
                return Ok(ApiResponseExtions.Succsess(update, "Product Update"));
            }
            catch
            {

                return StatusCode(500, ApiResponseExtions.Fail("Server Error Ocurde"));
            }
        }

        [HttpDelete("Product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                    return NotFound(ApiResponseExtions.Fail("Category not found or cannot be deleted"));
                return Ok(ApiResponseExtions.Succsess(null, "Category deleted"));
            }
            catch
            {

                return StatusCode(500, ApiResponseExtions.Fail("Server error occurde"));
            }
        }
    }
}