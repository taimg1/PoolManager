using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;

namespace PoolMS.Service.Service
{
    public class VisitService : IService<VisitDto, VisitCreateDto, VisitUpdateDto>
    {
        private readonly HttpClient _httpClient;
        public VisitService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<VisitDto> ItemList { get; set; } = new List<VisitDto>();
        public async Task AddAsync(VisitCreateDto entity)
        {
            await _httpClient.PostAsJsonAsync("api/visit/add", entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/visit/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<VisitDto>>("api/visit/list");
            if (result != null)
            {
                ItemList = result;
            }
        }

        public async Task<VisitDto> GetByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/visit/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<VisitDto>();
            }
            return null;
        }

        public async Task UpdateAsync(VisitUpdateDto entity)
        {
            await _httpClient.PutAsJsonAsync("api/visit/update", entity);
        }
    }
}
