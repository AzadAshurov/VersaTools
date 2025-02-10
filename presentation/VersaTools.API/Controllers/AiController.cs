using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.AiDTO;

namespace VersaTools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IAiService _aiService;

        public AiController(IAiService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("GetRegex")]
        public async Task<IActionResult> GetRegex([FromBody] GetRegexRequest request)
        {
            var result = await _aiService.GetRegex(request.Request);
            return Ok(result);
        }

        [HttpPost("GenerateSqlQuery")]
        public async Task<IActionResult> GenerateSqlQuery([FromBody] GenerateSqlQueryRequest request)
        {
            var result = await _aiService.GenerateSqlQuery(request.Description);
            return Ok(result);
        }

        [HttpPost("GeneratePassword")]
        public async Task<IActionResult> GeneratePassword([FromBody] GeneratePasswordRequest request)
        {
            var result = await _aiService.GeneratePassword(request.Length, request.IncludeSymbols, request.IncludeNumbers);
            return Ok(result);
        }

        [HttpPost("GenerateRandomData")]
        public async Task<IActionResult> GenerateRandomData([FromBody] GenerateRandomDataRequest request)
        {
            var result = await _aiService.GenerateRandomData(request.DataType);
            return Ok(result);
        }

        [HttpPost("FormatJson")]
        public async Task<IActionResult> FormatJson([FromBody] FormatJsonRequest request)
        {
            var result = await _aiService.FormatJson(request.Json);
            return Ok(result);
        }

        [HttpPost("FormatYaml")]
        public async Task<IActionResult> FormatYaml([FromBody] FormatYamlRequest request)
        {
            var result = await _aiService.FormatYaml(request.Yaml);
            return Ok(result);
        }

        [HttpPost("GenerateCodeSnippet")]
        public async Task<IActionResult> GenerateCodeSnippet([FromBody] GenerateCodeSnippetRequest request)
        {
            var result = await _aiService.GenerateCodeSnippet(request.Description, request.Language);
            return Ok(result);
        }

        [HttpPost("GenerateShellCommand")]
        public async Task<IActionResult> GenerateShellCommand([FromBody] GenerateShellCommandRequest request)
        {
            var result = await _aiService.GenerateShellCommand(request.Description, request.IsWindows);
            return Ok(result);
        }

        [HttpPost("GenerateMarkdown")]
        public async Task<IActionResult> GenerateMarkdown([FromBody] GenerateMarkdownRequest request)
        {
            var result = await _aiService.GenerateMarkdown(request.Text);
            return Ok(result);
        }

        [HttpPost("DecodeAbbreviation")]
        public async Task<IActionResult> DecodeAbbreviation([FromBody] DecodeAbbreviationRequest request)
        {
            var result = await _aiService.DecodeAbbreviation(request.Abbreviation);
            return Ok(result);
        }

        [HttpPost("GenerateSlogan")]
        public async Task<IActionResult> GenerateSlogan([FromBody] GenerateSloganRequest request)
        {
            var result = await _aiService.GenerateSlogan(request.Keywords);
            return Ok(result);
        }

        [HttpPost("GenerateProjectName")]
        public async Task<IActionResult> GenerateProjectName([FromBody] GenerateProjectNameRequest request)
        {
            var result = await _aiService.GenerateProjectName(request.Theme);
            return Ok(result);
        }

        [HttpPost("GenerateGreeting")]
        public async Task<IActionResult> GenerateGreeting([FromBody] GenerateGreetingRequest request)
        {
            var result = await _aiService.GenerateGreeting(request.Occasion, request.Recipient);
            return Ok(result);
        }

        [HttpPost("CreateToDoList")]
        public async Task<IActionResult> CreateToDoList([FromBody] CreateToDoListRequest request)
        {
            var result = await _aiService.CreateToDoList(request.Tasks);
            return Ok(result);
        }

        [HttpPost("GenerateProductDescription")]
        public async Task<IActionResult> GenerateProductDescription([FromBody] GenerateProductDescriptionRequest request)
        {
            var result = await _aiService.GenerateProductDescription(request.ProductName, request.Features);
            return Ok(result);
        }

        [HttpPost("ParaphraseText")]
        public async Task<IActionResult> ParaphraseText([FromBody] ParaphraseTextRequest request)
        {
            var result = await _aiService.ParaphraseText(request.Text);
            return Ok(result);
        }

        [HttpPost("GenerateSocialMediaPostIdea")]
        public async Task<IActionResult> GenerateSocialMediaPostIdea([FromBody] GenerateSocialMediaPostIdeaRequest request)
        {
            var result = await _aiService.GenerateSocialMediaPostIdea(request.Topic);
            return Ok(result);
        }

        [HttpPost("GenerateJoke")]
        public async Task<IActionResult> GenerateJoke([FromBody] GenerateJokeRequest request)
        {
            var result = await _aiService.GenerateJoke(request.Topic);
            return Ok(result);
        }

        [HttpPost("CreateResume")]
        public async Task<IActionResult> CreateResume([FromBody] CreateResumeRequest request)
        {
            var result = await _aiService.CreateResume(request.Skills, request.Experience);
            return Ok(result);
        }

        [HttpPost("GetInspirationalQuote")]
        public async Task<IActionResult> GetInspirationalQuote([FromBody] GetInspirationalQuoteRequest request)
        {
            var result = await _aiService.GetInspirationalQuote(request.Topic);
            return Ok(result);
        }
    }
}