using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SystemAPI.Services
{
    public interface IRespository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> GetByIdAsync(Guid entityId);
        Task<IEnumerable<TEntity>> GetAllAsync();
        //Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task SaveChangesAsync();
    }
}
