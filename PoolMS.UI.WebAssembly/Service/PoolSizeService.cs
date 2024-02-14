using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using PoolMS.UI.WebAssembly.Auth;

namespace PoolMS.UI.WebAssembly.Service
{
    public class PoolSizeService : IService<PoolSizeDto, PoolSizeCreateDto, PoolSizeUpdateDto>
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public PoolSizeService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public List<PoolSizeDto> ItemList { get; set; } = new List<PoolSizeDto>();

        public async Task AddAsync(PoolSizeCreateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PostAsJsonAsync("api/poolsize/add", entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/poolsize/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PoolSizeDto>>("api/poolsize/list");
            if (result is not null)
                ItemList = result;
        }

        public async Task<PoolSizeDto> GetByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/poolsize/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<PoolSizeDto>();
            else
                return null;
        }

        public async Task UpdateAsync(PoolSizeUpdateDto entity)
        {
            await _httpClient.PutAsJsonAsync("api/poolsize/update", entity);
        }
    }
}
