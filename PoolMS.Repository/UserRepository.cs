using Microsoft.EntityFrameworkCore;
using PoolMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Repository
{
    public class UserRepository
    {
        private readonly DbContext _context;
        public UserRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllA()
        {
            return await _context.Set<User>().ToListAsync();
        }
        public async Task<User> GetById(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }
        public async Task Add(User entity)
        {
            _context.Set<User>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(User entity)
        {
            _context.Set<User>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<User>().Attach(entity);
            }
            _context.Set<User>().Remove(entity);
            await _context.SaveChangesAsync();
        }


    }
}
