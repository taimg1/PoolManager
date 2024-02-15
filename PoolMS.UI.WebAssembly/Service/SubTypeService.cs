using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using PoolMS.UI.WebAssembly.Auth;

namespace PoolMS.UI.WebAssembly.Service
{
    public class SubTypeService : IService<SubTypeDto, SubTypeCreateDto, SubTypeUpdateDto>
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public SubTypeService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }
        public List<SubTypeDto> ItemList { get; set; } = new List<SubTypeDto>();

        public async Task AddAsync(SubTypeCreateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PostAsJsonAsync("api/subtype/add", entity);
        }

        public Task AddAsyncByUser(SubTypeCreateDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.DeleteAsync($"api/subtype/delete/{id}");
        }

        public async Task GetAllAsync()
        {

            var result = await _httpClient.GetFromJsonAsync<List<SubTypeDto>>("api/subtype/list");
            if (result != null)
            {
                ItemList = result;
            }

        }

        public async Task<SubTypeDto> GetByIdAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetAsync($"api/subtype/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<SubTypeDto>();
            else
                return null;
        }

        public Task GetByUser()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(SubTypeUpdateDto entity)
        {
            await _httpClient.PutAsJsonAsync("api/subtype/update", entity);
        }
    }
}
