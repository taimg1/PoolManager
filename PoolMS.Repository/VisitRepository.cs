using Microsoft.EntityFrameworkCore;
using PoolMS.Core;
using PoolMS.Core.Entities;
using PoolMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Repository
{
    public class VisitRepository: IRepository<Visit>
    {
        private readonly AppDbContext _context;
        public VisitRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Visit entity)
        {
            await _context.Visits.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Visit entity)
        {
            _context.Visits.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Visit>> GetAllAsync()
        {
            return await _context.Visits.ToListAsync();
        }
        public async Task<bool> ExistItem(int id)
        {
            return await _context.Visits.AnyAsync(p => p.Id == id);
        }
        public async Task<Visit> GetByIdAsync(int id)
        {
            return await _context.Visits.FindAsync(id);
        }

        public async Task UpdateAsync(Visit entity)
        {
            _context.Visits.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Visit> visits)
        {
            await _context.Visits.AddRangeAsync(visits);
            await _context.SaveChangesAsync();
        }
        
    }
}
