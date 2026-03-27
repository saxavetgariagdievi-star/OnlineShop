using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Response;

namespace OnlineShoppingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandController(IBrandService service)
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
                var data = await _service.GetAllAsync();
                return Ok(ApiResponseExtions.Succsess(data));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await _service.GetByIdAsync(id);
                if (data == null)
                    return NotFound(ApiResponseExtions.Fail("Brand not found"));

                return Ok(ApiResponseExtions.Succsess(data));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBrandDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponseExtions.Fail("Validation error"));

                var result = await _service.CreateAsync(dto);
                return Ok(ApiResponseExtions.Succsess(result, "Brand created"));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBrandDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResponseExtions.Fail("Validation error"));

                var result = await _service.UpdateAsync(id, dto);
                if (result == null)
                    return NotFound(ApiResponseExtions.Fail("Brand not found"));

                return Ok(ApiResponseExtions.Succsess(result, "Brand updated"));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                    return NotFound(ApiResponseExtions.Fail("Brand not found"));

                return Ok(ApiResponseExtions.Succsess(null, "Brand deleted"));
            }
            catch
            {
                return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
            }
        }
    }
}