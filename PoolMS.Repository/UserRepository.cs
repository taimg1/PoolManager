using Microsoft.EntityFrameworkCore;
using PoolMS.Core.Entities;
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
        public async Task<User> AuthorizeUserAsync(string Email, string Password)
        {
            var userData = await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == Email);
            if (userData == null)
            {
                return null;
            }
            else
            {
                string hashedInputPassword = ComputeHash(Password);

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

        public async Task<int> RegisterAsync(User user)
        {
            if (!IsValidEmail(user.Email))
            {
                throw new FormatException("Invalid email format. Please enter a valid email address.");
            }

            user.Password = ComputeHash(user.Password);
            user.Admin = false;
            var obj = _context.Set<User>().Add(user);
            await _context.SaveChangesAsync();
            return obj.Entity.Id;
        }


    }
}
