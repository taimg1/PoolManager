using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
namespace PoolMS.UI.WebAssembly.Service
{
    public class SubscriptionService : IService<SubscriptionDto, SubscriptionCreateDto, SubscriptionUpdateDto>
    {
        private readonly HttpClient _httpClient;
        public SubscriptionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<SubscriptionDto> ItemList { get; set; } = new List<SubscriptionDto>();

        public async Task AddAsync(SubscriptionCreateDto entity)
        {
            ; await _httpClient.PostAsJsonAsync("api/subscription/add", entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/subscription/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SubscriptionDto>>("api/subscription/list");
            if (result != null)
            {
                ItemList = result;
            }

        }

        public async Task<SubscriptionDto> GetByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/subscription/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<SubscriptionDto>();
            return null;
        }
        public async Task<SubscriptionDto> GetByUser()
        {
            var result = await _httpClient.GetAsync($"api/subscription/info");
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<SubscriptionDto>();
            return null;
        }

        public async Task UpdateAsync(SubscriptionUpdateDto entity)
        {
            await _httpClient.PutAsJsonAsync("api/subscription/update", entity);
        }
    }
}
