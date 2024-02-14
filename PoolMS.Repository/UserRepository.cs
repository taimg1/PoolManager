using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoolMS.Core;
using PoolMS.Core.Entities;
using PoolMS.Repository.Interface;
using PoolMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }
        public async Task<bool> ExistItem(int id)
        {
            return await _context.Users.AnyAsync(p => p.Id == id);
        }
        public async Task UpdateAsync(User entity)
        {
            _context.Set<User>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<User>().Attach(entity);
            }
            _context.Set<User>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<User> AuthorizeUserAsync(UserLoginDto userLoginDto)
        {
            var userData = await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == userLoginDto.Email);
            if (userData == null)
            {
                return null;
            }
            else
            {
                string hashedInputPassword = ComputeHash(userLoginDto.Password);

                if (userData.Password == hashedInputPassword)
                {
                    return userData;
                }
                else
                {
                    return null;
                }
            }
        }
        private string ComputeHash(string input)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }   
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<int> RegisterAsync(UserRegDto userrow)
        {
            
            if (!IsValidEmail(userrow.Email))
            {
                throw new FormatException("Invalid email format. Please enter a valid email address.");
            }
            var user = mapper.Map<User>(userrow);
            user.Password = ComputeHash(user.Password);
            user.Role = _context.Roles.FirstOrDefault(r => r.Title == "User");
            var obj = _context.Set<User>().Add(user);
            await _context.SaveChangesAsync();
            return obj.Entity.Id;
        }
        public async Task ChangeRole(int userId, int roleId)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == userId);
            user.Role = _context.Roles.FirstOrDefault(r => r.Id == roleId);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<User> users)
        {
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }

    }
}
