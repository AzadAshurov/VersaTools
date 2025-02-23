using Microsoft.EntityFrameworkCore;
using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Domain.Entitities;
using VersaTools.Persistence.DAL;
using VersaTools.Persistence.Implementations.Repositories.Generic;

namespace VersaTools.Persistence.Implementations.Repositories
{
    public class ComplaintRepository : Repository<Complaint>, IComplaintRepository
    {
        public ComplaintRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Complaint> GetBySpecialIdAsync(string specialId)
        {
            return await _context.Complaints.FirstOrDefaultAsync(x => x.SpecificId == specialId);
        }
    }
}
