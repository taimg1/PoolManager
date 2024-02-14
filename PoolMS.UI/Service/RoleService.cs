using PoolMS.Core.Entities;
using PoolMS.Repository.DTO;
using PoolMS.UI.Interfaces;
using System.Net;
using System.Runtime.InteropServices;

namespace PoolMS.UI.Service
{
    public class RoleService : IService<RoleDto, RoleCreateDto, RoleUpdateDto>
    {
        private readonly HttpClient _httpClient;
        public RoleService(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }

        public List<RoleDto> ItemList { get; set; } = new List<RoleDto>();
        public async Task AddAsync(RoleCreateDto entity)
        {
          await _httpClient.PostAsJsonAsync("api/role/add", entity);
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<RoleDto>>("api/role/list");
            if(result != null)
            {
                ItemList = result;
            }

        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/role/{id}");
            if(result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<RoleDto>();
            else
                return null;
        }

        public async Task UpdateAsync(RoleUpdateDto entity)
        {
           await _httpClient.PutAsJsonAsync("api/role/update", entity);
        }
    }
}
