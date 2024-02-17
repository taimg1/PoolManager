using PoolMS.Core;
using PoolMS.Core.Entities;
using PoolMS.Repository.Repository;
using System.Linq;

namespace PoolMS.API
{
    public class Seed
    {
        private readonly UserRepository _userRepository;
        
        public Seed(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //public async Task SeedDataContext()
        //{
        //    var users = await _userRepository.GetAllAsync();

        //    if (!users.Any())
        //    {
        //        User user = new User
        //        {
        //            Fname = "Admin",
        //            Sname = "Admin",
        //            Password = "SuperAdmin123",
        //            Email = "Admin@admin.com",
        //            Phone = "1234567890",
        //            AdminStatus = true
        //        };
        //        await _userRepository.RegisterAsync(user);
        //    }
        //}
    }
}
