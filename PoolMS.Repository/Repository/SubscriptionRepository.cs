using Microsoft.EntityFrameworkCore;
using PoolMS.Core;
using PoolMS.Core.Entities;
using PoolMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Repository.Repository
{
    public class SubscriptionRepository : IRepository<Subscription>
    {
        private readonly AppDbContext _context;
        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Subscription entity)
        {
            await _context.Subscriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subscription entity)
        {
            _context.Subscriptions.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistItem(int id)
        {
            return await _context.Subscriptions.AnyAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> GetByIdAsync(int id)
        {
            return await _context.Subscriptions.FindAsync(id);
        }

        public async Task UpdateAsync(Subscription entity)
        {
            _context.Subscriptions.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Subscription> subscriptions)
        {
            await _context.Subscriptions.AddRangeAsync(subscriptions);
            await _context.SaveChangesAsync();
        }
    }
}
