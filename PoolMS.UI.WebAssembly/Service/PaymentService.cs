using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using PoolMS.UI.WebAssembly.Auth;

namespace PoolMS.UI.WebAssembly.Service
{
    public class PaymentService : IService<PaymentDto, PaymentCreateDto, PaymentUpdateDto>
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        public PaymentService(HttpClient httpClient , AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;

        }

        public List<PaymentDto> ItemList { get; set; } = new List<PaymentDto>();

        public async Task AddAsync(PaymentCreateDto entity)
        {
            await _httpClient.PostAsJsonAsync("api/payment/pay", entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.DeleteAsync($"api/payment/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<PaymentDto>>("api/payment/list");
            if (result != null)
            {
                ItemList = result;
            }
        }
        public async Task GetByUser()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<PaymentDto>>($"api/payment/info");
            if (result is not null)
                ItemList = result;
        }
        public async Task<PaymentDto> GetByIdAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            return await _httpClient.GetFromJsonAsync<PaymentDto>($"api/payment/{id}");
        }

        public Task UpdateAsync(PaymentUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsyncByUser(PaymentCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
