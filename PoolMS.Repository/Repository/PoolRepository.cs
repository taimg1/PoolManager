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
    public class PoolRepository : IRepository<Pool>
    {
        private readonly AppDbContext _context;
        public PoolRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Pool entity)
        {
            await _context.Pools.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pool entity)
        {
            _context.Pools.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistItem(int id)
        {
            return await _context.Pools.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pool>> GetAllAsync()
        {
            return await _context.Pools.ToListAsync();
        }

        public async Task<Pool> GetByIdAsync(int id)
        {
            return await _context.Pools.FindAsync(id);
        }

        public async Task UpdateAsync(Pool entity)
        {
            _context.Pools.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Pool> pools)
        {
            await _context.Pools.AddRangeAsync(pools);
            await _context.SaveChangesAsync();
        }

    }
}
