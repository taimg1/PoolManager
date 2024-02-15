using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using PoolMS.UI.WebAssembly.Auth;

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
            throw new NotImplementedException();
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
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<RoleDto>();
            else
                return null;
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
