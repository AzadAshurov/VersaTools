using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Application.Abstractions.Repositories.Generic;
using VersaTools.Domain.Entitities;
using VersaTools.Persistence.DAL;
using VersaTools.Persistence.Implementations.Repositories.Generic;

namespace VersaTools.Persistence.Implementations.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
