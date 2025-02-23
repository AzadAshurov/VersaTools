using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Domain.Entitities;
using VersaTools.Persistence.DAL;
using VersaTools.Persistence.Implementations.Repositories.Generic;

namespace VersaTools.Persistence.Implementations.Repositories
{
    public class ResponseRepository : Repository<Response>, IResponseRepository
    {
        public ResponseRepository(AppDbContext context) : base(context)
        {
        }
    }

}
