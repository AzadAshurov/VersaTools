using Microsoft.AspNetCore.Mvc;
using VersaTools.Application.Abstractions.Services;

namespace VersaTools.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeyboardLayoutController : ControllerBase
    {
        private readonly IKeyboardLayoutService _keyboardLayoutService;

        public KeyboardLayoutController(IKeyboardLayoutService keyboardLayoutService)
        {
            _keyboardLayoutService = keyboardLayoutService;
        }

        [HttpGet("to-cyrillic")]
        public IActionResult ConvertToCyrillic([FromQuery] string input)
        {
            return Ok(_keyboardLayoutService.ConvertToCyrillic(input));
        }

        [HttpGet("to-latin")]
        public IActionResult ConvertToLatin([FromQuery] string input)
        {
            return Ok(_keyboardLayoutService.ConvertToLatin(input));
        }
    }
}
