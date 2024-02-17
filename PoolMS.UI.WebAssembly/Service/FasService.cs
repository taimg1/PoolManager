using PoolMS.Service.DTO;
using System.Net.Http.Json;

namespace PoolMS.UI.WebAssembly.Service
{
    public class FasService
    {
        private readonly HttpClient _httpClient;
        public FasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<PoolDto> ItemList { get; set; } = new List<PoolDto>();
        public async Task GetPopular()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PoolDto>>("api/fas/popular");
            if (result != null)
            {
                ItemList = result;
            }
        }   
        public async Task GetUnpopular()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PoolDto>>("api/fas/descending-popular");
            if (result != null)
            {
                ItemList = result;
            }
        }
        public async Task GetPoolSize()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PoolDto>>("api/fas/poolsize");
            if (result != null)
            {
                ItemList = result;
            }
        }
        public async Task GetDescendingPoolSize()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PoolDto>>("api/fas/descending-poolsize");
            if (result != null)
            {
                ItemList = result;
            }
        }
        public async Task GetTemperature()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PoolDto>>("api/fas/temperature");
            if (result != null)
            {
                ItemList = result;
            }
        }
        public async Task GetDescendingTemperature()
        {
            var result = await _httpClient.GetFromJsonAsync<List<PoolDto>>("api/fas/descending-temperature");
            if (result != null)
            {
                ItemList = result;
            }
        }
        public async Task Search(SearchDto searchDto)
        {
            var result = await _httpClient.PostAsJsonAsync("api/fas/search", searchDto);
            if (result.IsSuccessStatusCode)
            {
                ItemList = await result.Content.ReadFromJsonAsync<List<PoolDto>>();
            }
        }

    }
}
