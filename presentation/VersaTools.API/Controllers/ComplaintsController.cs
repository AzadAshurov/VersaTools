using Microsoft.AspNetCore.Mvc;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.ComplaintDTO;

namespace VersaTools.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IComplaintService _complaintService;

        public ComplaintsController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllComplaintsDTO getAllComplaintsDTO)
        {
            var complaints = await _complaintService.GetAllAsync(getAllComplaintsDTO);
            return Ok(complaints);
        }

        [HttpGet("{specialId}")]
        public async Task<IActionResult> GetBySpecialId(string specialId)
        {
            var complaint = await _complaintService.GetBySpecialIdAsync(specialId);
            if (complaint == null)
                return NotFound("Complaint not found");

            return Ok(complaint);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateComplaintDTO complaintDTO)
        {
            await _complaintService.CreateAsync(complaintDTO);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("ban-or-ignore")]
        public async Task<IActionResult> BanOrIgnore([FromBody] BanOrIgnoreComplaintDTO banOrIgnoreDTO)
        {
            await _complaintService.BanOrIgnoreComplaintAsync(banOrIgnoreDTO);
            return NoContent();
        }
    }

}
