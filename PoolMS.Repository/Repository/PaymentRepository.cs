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
    public class PaymentRepository : IRepository<Payment>
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Payment entity)
        {
            await _context.Payments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Payment entity)
        {
            _context.Payments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistItem(int id)
        {
            return await _context.Payments.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public Task UpdateAsync(Payment entity)
        {
            _context.Payments.Update(entity);
            return _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Payment> payments)
        {
            await _context.Payments.AddRangeAsync(payments);
            await _context.SaveChangesAsync();
        }
    }
}
