using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApi.DTOs;
using OnlineShoppingApi.Interfaces;
using OnlineShoppingApi.Response;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        try
        {
            var success = await _userService.RegisterAsync(dto);
            if (!success)
                return BadRequest(ApiResponseExtions.Fail("User already exists"));

            return Ok(ApiResponseExtions.Succsess(null, "Register successful"));
        }
        catch
        {
            return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        try
        {
            var token = await _userService.LoginAsync(dto);
            if (token == null)
                return Unauthorized(ApiResponseExtions.Fail("Email və ya şifrə yanlışdır"));

            return Ok(ApiResponseExtions.Succsess(new { Token = token }, "Login successful"));
        }
        catch
        {
            return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
        }
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> Me()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(ApiResponseExtions.Fail("User not authenticated"));

            var userId = int.Parse(userIdClaim.Value);
            var user = await _userService.GetByIdAsync(userId);

            if (user == null)
                return NotFound(ApiResponseExtions.Fail("User not found"));

            return Ok(ApiResponseExtions.Succsess(user));
        }
        catch
        {
            return StatusCode(500, ApiResponseExtions.Fail("Server error occurred"));
        }
    }
}