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
    public class RoleRepository : IRepository<Role>
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task AddAsync(Role entity)
        {
            await _context.Roles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role entity)
        {
            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistItem(int id)
        {
            return await _context.Roles.AnyAsync(p => p.Id == id); 
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task UpdateAsync(Role entity)
        {
            _context.Roles.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Role> entities)
        {
            await _context.Roles.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
