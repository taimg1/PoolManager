using PoolMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Service.Interface
{
    public interface ISubscriptionService
    {
        List<SubscriptionDto> ItemList { get; set; }
        Task GetAllAsync();
        Task<SubscriptionDto> GetByIdAsync(int id);
        Task AddAsync(SubscriptionCreateDto entity);
        Task UpdateAsync(SubscriptionUpdateDto entity);
        Task DeleteAsync(int id);
        Task GetByUser();
        Task AddByUser(SubscriptionCreateUserDto entity);
    }
}
