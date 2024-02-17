using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;
using PoolMS.UI.WebAssembly.Auth;
using Azure;
namespace PoolMS.UI.WebAssembly.Service
{
    public class ReservationService : IService<ReservationDto, ReservationCreateDto, ReservationUpdateDto>
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        public ReservationService(HttpClient httpClient, AuthService authService)
        {

            _httpClient = httpClient;
            _authService = authService;

        }

        public List<ReservationDto> ItemList { get; set; } = new List<ReservationDto>();

        public async Task AddAsync(ReservationCreateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PostAsJsonAsync("api/reservation/admin/add", entity);
        }

        public async Task AddAsyncByUser(ReservationCreateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PostAsJsonAsync("api/reservation/add", entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.DeleteAsync($"api/reservation/delete/{id}");
        }

        public async Task GetAllAsync()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<ReservationDto>>("api/reservation/list");
            if (result != null)
            {
                ItemList = result;
            }
        }

        public async Task<ReservationDto> GetByIdAsync(int id)
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetAsync($"api/reservation/{id}");
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {result.StatusCode}");
            }
            return await result.Content.ReadFromJsonAsync<ReservationDto>();
        
        }

        public async Task GetByUser()
        {
            await _authService.SetJwtTokenInHeader();
            var result = await _httpClient.GetFromJsonAsync<List<ReservationDto>>($"api/reservation/info");
            if (result is not null)
            {
                ItemList = result;
            }
        }

        public async Task UpdateAsync(ReservationUpdateDto entity)
        {
            await _authService.SetJwtTokenInHeader();
            await _httpClient.PutAsJsonAsync("api/reservation/update", entity);
        }
    }
}
