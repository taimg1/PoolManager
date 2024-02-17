using PoolMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Service.Interface
{
    public  interface IPoolService
    {
        List<PoolDto> ItemList { get; set; }
        Task GetAllAsync();
        Task<PoolDto> GetByIdAsync(int id);
        Task AddAsync(PoolCreateDto entity);
        Task UpdateAsync(PoolUpdateDto entity);
        Task DeleteAsync(int id);
        Task GetByUser();
        Task AddAsyncByUser(PoolCreateDto entity);
        Task<HttpResponseMessage> GetPoolUsageReport(int Id);
    }
}
