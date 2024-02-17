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
    public class ReservationRepository : IRepository<Reservation>
    {
        private readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Reservation entity)
        {
            await _context.Reservations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation entity)
        {
            _context.Reservations.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistItem(int id)
        {
            return await _context.Reservations.AnyAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task UpdateAsync(Reservation entity)
        {
            _context.Reservations.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Reservation> reservations)
        {
            await _context.Reservations.AddRangeAsync(reservations);
            await _context.SaveChangesAsync();
        }


    }
}
