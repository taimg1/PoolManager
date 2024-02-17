using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using PoolMS.UI.WebAssembly.Auth;
using Azure;

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

        public Task AddAsyncByUser(PoolSizeCreateDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.DeleteAsync($"api/poolsize/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<PoolSizeDto>>("api/poolsize/list");
            if (result is not null)
                ItemList = result;
        }

        public async Task<PoolSizeDto> GetByIdAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            var response = await _httpClient.GetAsync($"api/poolsize/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }
            return await response.Content.ReadFromJsonAsync<PoolSizeDto>();
        
        }

        public Task GetByUser()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PoolSizeUpdateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PutAsJsonAsync("api/poolsize/update", entity);
        }
    }
}
