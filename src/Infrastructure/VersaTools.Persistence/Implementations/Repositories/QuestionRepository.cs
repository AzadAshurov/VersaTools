﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<Question> GetBySpecialIdAsync(string specialId)
        {
            return await _context.Questions.FirstOrDefaultAsync(x => x.SpecificId == specialId);
        }
    }
}
