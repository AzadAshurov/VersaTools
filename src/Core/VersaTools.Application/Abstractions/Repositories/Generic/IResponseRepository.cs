using VersaTools.Domain.Entitities;

namespace VersaTools.Application.Abstractions.Repositories.Generic
{
    public interface IResponseRepository : IRepository<Response>
    {
        Task<Response> GetBySpecialIdAsync(string specialId);
    }

}
