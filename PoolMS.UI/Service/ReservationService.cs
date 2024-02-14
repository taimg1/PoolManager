using PoolMS.Core.Entities;
using PoolMS.Service.DTO;
using PoolMS.UI.Interfaces;
using System.Net;
namespace PoolMS.UI.Service
{
    public class ReservationService : IService<ReservationDto, ReservationCreateDto, ReservationUpdateDto>
    {
        private readonly HttpClient _httpClient;
        public ReservationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<ReservationDto> ItemList { get; set ; } = new List<ReservationDto>();   

        public async Task AddAsync(ReservationCreateDto entity)
        {
            await _httpClient.PostAsJsonAsync("api/reservation/add", entity);   
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/reservation/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ReservationDto>>("api/reservation/list");
            if(result != null)
            {
                ItemList = result;
            }
        }

        public async Task<ReservationDto> GetByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/reservation/{id}");
            if(result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<ReservationDto>();
            else
                return null;
        }

        public async Task UpdateAsync(ReservationUpdateDto entity)
        {
            await _httpClient.PutAsJsonAsync("api/reservation/update", entity);
        }
    }
}
