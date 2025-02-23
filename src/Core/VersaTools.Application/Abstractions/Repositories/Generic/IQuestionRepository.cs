using VersaTools.Domain.Entitities;

namespace VersaTools.Application.Abstractions.Repositories.Generic
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<Question> GetBySpecialIdAsync(string specialId);
    }
}
