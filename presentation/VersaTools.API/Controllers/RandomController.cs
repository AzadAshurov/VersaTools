using Microsoft.AspNetCore.Mvc;
using VersaTools.Application.Abstractions.Services;

namespace VersaTools.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandomizerController : ControllerBase
    {
        private readonly IRandomService _randomService;

        public RandomizerController(IRandomService randomService)
        {
            _randomService = randomService;
        }

        [HttpGet("numbers")]
        public IActionResult GetRandomNumbers([FromQuery] int min, [FromQuery] int max, [FromQuery] int count, [FromQuery] bool unique)
        {
            try
            {
                return Ok(_randomService.GetRandomNumbers(min, max, count, unique));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("choice")]
        public IActionResult GetRandomChoice([FromQuery] string[] values)
        {
            try
            {
                return Ok(_randomService.GetRandomChoice(values));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}