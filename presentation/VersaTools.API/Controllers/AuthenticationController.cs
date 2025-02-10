
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.AppUsers;


namespace VersaTools.Persistence.Implementations.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Register([FromForm] RegisterDto userDto)
        {
            await _service.RegisterAsync(userDto);
            return NoContent();
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Login([FromForm] LoginDto userDto)
        {
            return Ok(await _service.LoginAsync(userDto));
        }
        [Authorize]
        [HttpPost("[Action]")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _service.LogoutAsync(userId);
            return NoContent();
        }
    }

}