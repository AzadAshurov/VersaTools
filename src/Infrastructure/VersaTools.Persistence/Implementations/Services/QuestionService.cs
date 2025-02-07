using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.QuestionDTO;
using VersaTools.Application.DTOs.ResponseDTO;
using VersaTools.Domain.Entitities;

namespace VersaTools.Persistence.Implementations.Services
{
   public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task CreateAsync(CreateQuestionDTO questionDTO)
        {
            Question question = new Question
            {
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                UpdatedAt = DateTime.Now,
                MainText = questionDTO.MainText,
                Title = questionDTO.Title,
                SpecificId = "QUES" + Guid.NewGuid().ToString()
            };
          await _questionRepository.AddAsync(question);
            await _questionRepository.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            Question question = await _questionRepository.GetByIdAsync(id, "Responses");
            _questionRepository.Delete(question);
           await _questionRepository.SaveChangesAsync();

        }

        public async Task<IEnumerable<QuestionsDTO>> GetAllAsync(int page, int take)
        {
            ICollection<Question> questions = await _questionRepository.GetAll()
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            IEnumerable<QuestionsDTO> dto = questions.Select(x => new QuestionsDTO(x.Title));
            return dto;
        }


        public async Task<GetQuestionDTO> GetByIdAsync(int id)
        {
            Question question = await _questionRepository.GetByIdAsync(id, "Responses");

            var filteredResponses = question.Responses
                .Where(x => x.QuestionId == question.Id) 
                .Select(x => new GetResponseDTO(x.SpecificId, x.ResponseText))
                .ToList();

            return new GetQuestionDTO(question.Title, question.SpecificId, question.MainText, filteredResponses);
        }


        public async Task UpdateQuestionAsync(int id, UpdateQuestionDTO questionDTO)
        {
            Question question = await _questionRepository.GetByIdAsync(id);
            question.MainText = questionDTO.MainText;
            question.UpdatedAt = DateTime.Now;
            question.Title = questionDTO.Title;
             _questionRepository.Update(question);
            await _questionRepository.SaveChangesAsync();


        }
    }
}
