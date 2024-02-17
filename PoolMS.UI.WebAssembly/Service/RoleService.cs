using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using PoolMS.UI.WebAssembly.Auth;
using System.Buffers;

namespace PoolMS.UI.WebAssembly.Service
{
    public class RoleService : IService<RoleDto, RoleCreateDto, RoleUpdateDto>
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        public RoleService(HttpClient httpclient, AuthService authService)
        {
            _httpClient = httpclient;
            _authService = authService;
        }

        public List<RoleDto> ItemList { get; set; } = new List<RoleDto>();
        public async Task AddAsync(RoleCreateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PostAsJsonAsync("api/role/add", entity);
        }

        public Task AddAsyncByUser(RoleCreateDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            var response = await _httpClient.DeleteAsync($"api/role/delete/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }
        }

        public async Task GetAllAsync()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<RoleDto>>("api/role/list");
            if (result != null)
            {
                ItemList = result;
            }

        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
  
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetAsync($"api/role/{id}");
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine(id+1);
                return null;
            }
            if (!result.IsSuccessStatusCode)
            {
     
                throw new HttpRequestException($"Unexpected HTTP status code {result.StatusCode}");
            }

            return await result.Content.ReadFromJsonAsync<RoleDto>();
        }
        public async Task UpdateAsync(RoleUpdateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PutAsJsonAsync("api/role/update", entity);
        }

        Task IService<RoleDto, RoleCreateDto, RoleUpdateDto>.GetByUser()
        {
            throw new NotImplementedException();
        }
    }
}
