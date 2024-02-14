using PoolMS.Core.Entities;
using PoolMS.Service.DTO;
using PoolMS.UI.Interfaces;
using System.Net;

namespace PoolMS.UI.Service
{
    public class PaymentService : IService<PaymentDto, PaymentCreateDto, Payment>
    {
        private readonly HttpClient _httpClient;
        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<PaymentDto> ItemList { get; set; } = new List<PaymentDto>();

        public async Task AddAsync(PaymentCreateDto entity)
        {
           await _httpClient.PostAsJsonAsync("api/payment/pay", entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/payment/delete/{id}");  
        }

        public async Task GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PaymentDto>>("api/payment/list");
            if(result != null)
            {
                ItemList = result;
            }
        }
        public async Task<PaymentDto> GetByUser()
        {
            var result = await _httpClient.GetAsync($"api/payment/info");
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<PaymentDto>();
            return null;
        }
        public async Task<PaymentDto> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PaymentDto>($"api/payment/{id}");
        }

        public async Task UpdateAsync(Payment entity)
        {
           await _httpClient.PutAsJsonAsync("api/payment/update", entity);
        }
    }
}
