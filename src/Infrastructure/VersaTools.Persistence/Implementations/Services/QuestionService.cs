using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                SpecificId = Guid.NewGuid().ToString()
            };
          await _questionRepository.AddAsync(question);
            await _questionRepository.SaveChangesAsync();
        }

        public Task DeleteQuestionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuestionsDTO>> GetAllAsync(int page, int take)
        {
            throw new NotImplementedException();
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


        public Task UpdateQuestionAsync(int id, UpdateQuestionDTO questionDTO)
        {
            throw new NotImplementedException();
        }
    }
}
