using PoolMS.Core.Entities;
using PoolMS.Service.DTO;
using PoolMS.UI.Interfaces;
using System.Net;

namespace PoolMS.UI.Service
{
    public class PoolService : IService<PoolDto, PoolCreateDto, PoolUpdateDto>
    {
        private readonly HttpClient _httpClient;
        public PoolService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<PoolDto> ItemList { get; set; } = new List<PoolDto>();

        public async Task AddAsync(PoolCreateDto entity)
        {
            await _httpClient.PostAsJsonAsync("api/pool/add", entity);

        }

        public async Task DeleteAsync(int id)
        {
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
            var result = await _httpClient.GetAsync($"api/pool/{id}");
            if(result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<PoolDto>();
            else
                return null;
        }

        public async Task UpdateAsync(PoolUpdateDto entity)
        {
            await _httpClient.PutAsJsonAsync("api/pool/update", entity);
        }
    }
}
