using PoolMS.Core.Entities;
using PoolMS.Service.DTO;
using PoolMS.UI.Interfaces;
using System.Net;

namespace PoolMS.UI.Service
{
    public class SubTypeService : IService<SubTypeDto, SubTypeCreateDto, SubTypeUpdateDto>
    {
        private readonly HttpClient _httpClient;
        public SubTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<SubTypeDto> ItemList { get; set;  } = new List<SubTypeDto>();

        public async Task AddAsync(SubTypeCreateDto entity)
        {
            await _httpClient.PostAsJsonAsync("api/subtype/add", entity);
        }

        public async Task DeleteAsync(int id)
        {
           await _httpClient.DeleteAsync($"api/subtype/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SubTypeDto>>("api/subtype/list");
            if(result != null)
            {
                ItemList = result;
            }

        }

        public async Task<SubTypeDto> GetByIdAsync(int id)
        {
           var result = await _httpClient.GetAsync($"api/subtype/{id}");
            if(result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<SubTypeDto>();
            else
                return null;
        }

        public async Task UpdateAsync(SubTypeUpdateDto entity)
        {
            await _httpClient.PutAsJsonAsync("api/subtype/update", entity);
        }
    }
}
