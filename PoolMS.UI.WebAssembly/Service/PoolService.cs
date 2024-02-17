using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using PoolMS.UI.WebAssembly.Auth;

using Azure;

namespace PoolMS.UI.WebAssembly.Service
{
    public class PoolService : IPoolService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        public PoolService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public List<PoolDto> ItemList { get; set; } = new List<PoolDto>();

        public async Task AddAsync(PoolCreateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PostAsJsonAsync("api/pool/add", entity);

        }

        public Task AddAsyncByUser(PoolCreateDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.DeleteAsync($"api/pool/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PoolDto>>("api/pool/list");
            if (result is not null)
                ItemList = result;
        }

        public async Task<PoolDto> GetByIdAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            var response = await _httpClient.GetAsync($"api/pool/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<PoolDto>();
        }

        public Task GetByUser()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PoolUpdateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PutAsJsonAsync("api/pool/update", entity);
        }
        public async Task<HttpResponseMessage> GetPoolUsageReport(int Id)
        {
            await _authService.SetJwtTokenInHeader();
            var response = await _httpClient.GetAsync($"api/pool/poolusagereport/{Id}");
            return response;
        }
    }
}
