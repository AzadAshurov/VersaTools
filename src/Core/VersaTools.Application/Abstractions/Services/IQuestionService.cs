using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Application.DTOs.QuestionDTO;

namespace VersaTools.Application.Abstractions.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionsDTO>> GetAllAsync(int page, int take);
        Task<GetQuestionDTO> GetByIdAsync(int id);
        Task CreateAsync(CreateQuestionDTO colorDTO);
        Task UpdateQuestionAsync(int id, UpdateQuestionDTO questionDTO);
        Task DeleteQuestionAsync(int id);
    }
}
