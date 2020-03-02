using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemAPI.Services
{
    public interface IRespository<TEntity> where TEntity : class
    {
        //Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);

        Task Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        ValueTask<TEntity> GetByIdAsync(Guid entityId);
        Task SaveChangesAsync();

        Task Update(TEntity entity);

        bool Exist(Guid entityId);
    }
}