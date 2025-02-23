
using Microsoft.EntityFrameworkCore;
using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Application.Abstractions.Services;
using VersaTools.Application.DTOs.ResponseDTO;
using VersaTools.Domain.Entitities;

namespace VersaTools.Persistence.Implementations.Services
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepository _responseRepository;
        private readonly IQuestionRepository _questionRepository;

        public ResponseService(IResponseRepository responseRepository, IQuestionRepository questionRepository)
        {
            _responseRepository = responseRepository;
            _questionRepository = questionRepository;
        }

        public async Task CreateAsync(CreateResponseDTO responseDTO)
        {

            var question = await _questionRepository.GetByIdAsync(responseDTO.QuestionId);
            if (question == null)
                throw new Exception("Question not found");

            Response response = new Response
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false,
                ResponseText = responseDTO.ResponseText,
                QuestionId = responseDTO.QuestionId,
                SpecificId = "RESP" + Guid.NewGuid().ToString()
            };

            await _responseRepository.AddAsync(response);
            await _responseRepository.SaveChangesAsync();
        }

        public async Task DeleteResponseAsync(int id)
        {
            Response response = await _responseRepository.GetByIdAsync(id);
            if (response == null)
                throw new Exception("Response not found");

            _responseRepository.Delete(response);
            await _responseRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResponsesDTO>> GetAllAsync(int page, int take)
        {
            var responses = await _responseRepository.GetAll()
                                .Skip((page - 1) * take)
                                .Take(take)
                                .ToListAsync();

            return responses.Select(r => new ResponsesDTO(r.SpecificId, r.ResponseText));
        }

        public async Task<GetResponseDTO> GetByIdAsync(int id)
        {
            Response response = await _responseRepository.GetByIdAsync(id, "Question");
            if (response == null)
                throw new Exception("Response not found");

            return new GetResponseDTO(response.SpecificId, response.ResponseText);
        }

        public async Task UpdateResponseAsync(int id, UpdateResponseDTO responseDTO)
        {
            Response response = await _responseRepository.GetByIdAsync(id);
            if (response == null)
                throw new Exception("Response not found");

            response.ResponseText = responseDTO.ResponseText;
            response.UpdatedAt = DateTime.Now;

            _responseRepository.Update(response);
            await _responseRepository.SaveChangesAsync();
        }
    }

}
