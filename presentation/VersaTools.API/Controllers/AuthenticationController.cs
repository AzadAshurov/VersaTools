using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.AppUsers;

namespace VersaTools.API.Controllers
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

        [HttpPost]
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
    }

}
