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
    public class PoolSizeRepository : IRepository<PoolSize>
    {
        private readonly AppDbContext _context;
        public PoolSizeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(PoolSize entity)
        {
            await _context.PoolSizes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PoolSize entity)
        {
            _context.PoolSizes.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistItem(int id)
        {
            return await _context.PoolSizes.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PoolSize>> GetAllAsync()
        {
            return await _context.PoolSizes.ToListAsync();
        }

        public async Task<PoolSize> GetByIdAsync(int id)
        {
            return await _context.PoolSizes.FindAsync(id);
        }

        public async Task UpdateAsync(PoolSize entity)
        {
            _context.PoolSizes.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<PoolSize> poolSizes)
        {
            await _context.PoolSizes.AddRangeAsync(poolSizes);
            await _context.SaveChangesAsync();
        }
    }
}
