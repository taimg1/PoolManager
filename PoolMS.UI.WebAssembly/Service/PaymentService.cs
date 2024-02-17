using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using PoolMS.UI.WebAssembly.Auth;

namespace PoolMS.UI.WebAssembly.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        public PaymentService(HttpClient httpClient , AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;

        }

        public List<PaymentDto> ItemList { get; set; } = new List<PaymentDto>();

        public async Task<bool> AddAsync(PaymentCreateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.PostAsJsonAsync("api/payment/pay", entity);
            return result.IsSuccessStatusCode;
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
            var response = await _httpClient.GetAsync($"api/payment/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<PaymentDto>();
        }


        public async Task<bool> AddAsyncByUser(PaymentCreateUserDto amout)
        {
            await _authService.SetJwtTokenInHeader();
            Console.WriteLine(amout.Amount);
            var result = await _httpClient.PostAsJsonAsync("api/payment/pay", amout);
            return result.IsSuccessStatusCode;
        }
     
    }
}
