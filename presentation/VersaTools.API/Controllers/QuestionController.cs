using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.QuestionDTO;
using VersaTools.Domain.Entitities;

namespace VersaTools.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionsController : Controller
    {
        private readonly IRepository<Question> _repository;
        private readonly IQuestionService _questionService;

        public QuestionsController(IRepository<Question> repository, IQuestionService questionService)
        {
            _repository = repository;
            _questionService = questionService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 30)
        {
            return StatusCode(StatusCodes.Status200OK, await _questionService.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            var questionDTO = await _questionService.GetByIdAsync(id);

            if (questionDTO == null) return NotFound();

            return StatusCode(StatusCodes.Status200OK, questionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateQuestionDTO questionDTO)
        {
            await _questionService.CreateAsync(questionDTO);
            //    return BadRequest();

            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateQuestionDTO questionDTO)
        {
            if (id < 1)
                return BadRequest();

            await _questionService.UpdateQuestionAsync(id, questionDTO);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest();
            await _questionService.DeleteQuestionAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
