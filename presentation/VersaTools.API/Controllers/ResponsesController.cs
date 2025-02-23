using Microsoft.AspNetCore.Mvc;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.ResponseDTO;

namespace VersaTools.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ResponsesController : Controller
    {
        private readonly IResponseService _responseService;

        public ResponsesController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 30)
        {
            var responses = await _responseService.GetAllAsync(page, take);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            var responseDTO = await _responseService.GetByIdAsync(id);
            if (responseDTO == null) return NotFound();

            return StatusCode(StatusCodes.Status200OK, responseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateResponseDTO responseDTO)
        {
            await _responseService.CreateAsync(responseDTO);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateResponseDTO responseDTO)
        {
            if (id < 1) return BadRequest();

            await _responseService.UpdateResponseAsync(id, responseDTO);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            await _responseService.DeleteResponseAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
