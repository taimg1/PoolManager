using PoolMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Service.Interface
{
    public interface IPaymentService
    {
        List<PaymentDto> ItemList { get; set; }
        Task GetAllAsync();
        Task<PaymentDto> GetByIdAsync(int id);
        Task<bool> AddAsync(PaymentCreateDto entity);
        Task DeleteAsync(int id);
        Task GetByUser();
        Task<bool> AddAsyncByUser(PaymentCreateUserDto amout);
        Task<HttpResponseMessage> GetPaymentReport(int id);
        Task<HttpResponseMessage> GetMonthlyIncomeReport();

    }
}
