using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VersaTools.Domain.Entitities.Base;

namespace VersaTools.Application.Abstractions.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public IQueryable<T> GetAll(
       Expression<Func<T, bool>>? expression = null,
       Expression<Func<T, object>>? orderExpression = null,
       int skip = 0,
       int take = 0,
       bool isDescending = false,
       bool isTracking = false,
       bool ignoreQuery = false,
       params string[]? includes);
        Task<T> GetByIdAsync(int id, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        Task<int> SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> anyExpression);
    }
}
