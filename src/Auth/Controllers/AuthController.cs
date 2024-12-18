using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(UserService userService, JwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid registration data.");
            }

            try
            {
                // Register the user
                var user = await _userService.RegisterUserAsync(model);
                if (user == null)
                {
                    return BadRequest("User registration failed.");
                }

                // Generate JWT token after successful registration
                var token = _jwtTokenService.GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid login data.");
            }

            try
            {
                // Validate the user login
                var user = await _userService.ValidateUserAsync(model);
                if (user == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                // Generate JWT token after successful login
                var token = _jwtTokenService.GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // POST: api/auth/refresh-token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Invalid refresh token.");
            }

            try
            {
                // Assume the userId is retrieved from the claims of the authenticated user
                var userId = "userId"; // Placeholder, replace with actual userId from claims
                var token = await _userService.RefreshJwtTokenAsync(refreshToken, userId);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
