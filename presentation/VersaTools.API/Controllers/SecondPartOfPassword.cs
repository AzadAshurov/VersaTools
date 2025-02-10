using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.AiDTO;
using VersaTools.Infrastructure.Implementations.Services;

namespace VersaTools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecondPartOfPassword : ControllerBase
    {
        private readonly ISecondPartOfPasswordService _service;

        public SecondPartOfPassword(ISecondPartOfPasswordService service)
        {
           _service = service;
        }
        [HttpPost("Password")]
        public async Task<IActionResult> FormatJson(string input,string key)
        {
           
            return Ok(_service.CreatePassword(input, key));
        }
    }
}
