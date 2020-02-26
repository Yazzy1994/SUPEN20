using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SUPEN20DB.DbContexts;
using SUPEN20DB.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SystemAPI.Services
{
    public class SUPEN20Respository<TEntity>: IRespository<TEntity>  where TEntity : class 
    {
        private readonly SUPEN20DbContext _context;
        

        public SUPEN20Respository(SUPEN20DbContext context)
        {
            _context = context;
        
        }

        public async Task AddAsync(TEntity entity)
        {
             await _context.Set<TEntity>().AddAsync(entity);

        }

        public Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await  _context.Set<TEntity>().ToListAsync();

        }

        public ValueTask<TEntity> GetByIdAsync(Guid entityId)
        {
            return _context.Set<TEntity>().FindAsync(entityId);
        }

        //public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return await _context.Set<T>().Where(predicate).ToArrayAsync(); 
        //}

        public Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
