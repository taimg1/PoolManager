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
    public class SubTypeRepository : IRepository<SubType>
    {
        private readonly AppDbContext _context;
        public SubTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SubType entity)
        {
            await _context.SubTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SubType entity)
        {
            _context.SubTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistItem(int id)
        {
            return await _context.SubTypes.AnyAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<SubType>> GetAllAsync()
        {
            return await _context.SubTypes.ToListAsync();
        }

        public async Task<SubType> GetByIdAsync(int id)
        {
            return await _context.SubTypes.FindAsync(id);
        }

        public async Task UpdateAsync(SubType entity)
        {
            _context.SubTypes.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<SubType> subTypes)
        {
            await _context.SubTypes.AddRangeAsync(subTypes);
            await _context.SaveChangesAsync();
        }
    }
}
