using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using PoolMS.UI.WebAssembly.Auth;

namespace PoolMS.UI.WebAssembly.Service
{
    public class VisitService : IService<VisitDto, VisitCreateDto, VisitUpdateDto>
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        public VisitService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }
        public List<VisitDto> ItemList { get; set; } = new List<VisitDto>();
        public async Task AddAsync(VisitCreateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PostAsJsonAsync("api/visit/add", entity);
        }

        public Task AddAsyncByUser(VisitCreateDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.DeleteAsync($"api/visit/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<VisitDto>>("api/visit/list");
            if (result != null)
            {
                ItemList = result;
            }
        }

        public async Task<VisitDto> GetByIdAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetAsync($"api/visit/{id}");
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {result.StatusCode}");
            }
            return await result.Content.ReadFromJsonAsync<VisitDto>();
       
        }

        public async Task GetByUser()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<VisitDto>>($"api/visit/info");
            if (result is not null)
            {
                ItemList = result;
            }
        }

        public async Task UpdateAsync(VisitUpdateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PutAsJsonAsync("api/visit/update", entity);
        }
    }
}
