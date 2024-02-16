using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using PoolMS.UI.WebAssembly.Auth;
using ServiceStack.Text;
namespace PoolMS.UI.WebAssembly.Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        public SubscriptionService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public List<SubscriptionDto> ItemList { get; set; } = new List<SubscriptionDto>();

        public async Task AddAsync(SubscriptionCreateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PostAsJsonAsync("api/subscription/user/add", entity);
        }


        public async Task AddByUser(SubscriptionCreateUserDto entity)
        {
            Console.WriteLine(JsonContent.Create(entity));
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PostAsJsonAsync("api/subscription/add", entity);
        }
        public async Task DeleteAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.DeleteAsync($"api/subscription/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<SubscriptionDto>>("api/subscription/list");
            if (result != null)
            {
                ItemList = result;
            }

        }

        public async Task<SubscriptionDto> GetByIdAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetAsync($"api/subscription/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<SubscriptionDto>();
            return null;
        }
        public async Task GetByUser()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<SubscriptionDto>>($"api/subscription/info");
            if (result is not null)
            {
                ItemList = result;
            }
        }

        public async Task UpdateAsync(SubscriptionUpdateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PutAsJsonAsync("api/subscription/update", entity);
        }

    }
}
