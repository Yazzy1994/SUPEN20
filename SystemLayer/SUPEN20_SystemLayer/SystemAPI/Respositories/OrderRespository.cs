using Microsoft.EntityFrameworkCore;
using SUPEN20DB.DbContexts;
using SUPEN20DB.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemAPI.Services
{
    public class OrderRespository : IRespository<Order>
    {
        private readonly SUPEN20DbContext _context;

        public OrderRespository(SUPEN20DbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Set<Order>().AddAsync(order);
        }

        public Task Delete(Order order)
        {
            _context.Set<Order>().Remove(order);
            return _context.SaveChangesAsync();
        }

        public bool Exist(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            return _context.Orders.Any(o => o.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            bool IncludedItems = true;

            if (IncludedItems)
            {
                return await _context.Orders.Include(o => o.OrderItems).ThenInclude(i => i.Product).ToListAsync();
            }
            else
            {
                return await _context.Orders.ToListAsync();
            }
        }

        public async ValueTask<Order> GetByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Product)
                .Where(o => o.OrderId == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}