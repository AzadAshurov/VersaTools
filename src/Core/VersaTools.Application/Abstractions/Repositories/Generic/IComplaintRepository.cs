using VersaTools.Domain.Entitities;

namespace VersaTools.Application.Abstractions.Repositories.Generic
{
    public interface IComplaintRepository : IRepository<Complaint>
    {
        Task<Complaint> GetBySpecialIdAsync(string specialId);
    }
}
