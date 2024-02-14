using PoolMS.Service.Interface;
using PoolMS.Service.DTO;
using System.Net;
using System.Net.Http.Json;

namespace PoolMS.UI.WebAssembly.Service
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public List<UserDto> ItemList { get; set; } = new List<UserDto>();
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/login", userLoginDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserDto>();
                return result;
            }
            return null;
        }
        public async Task Registration(UserRegDto userRegisterDto)
        {
            await _httpClient.PostAsJsonAsync("api/user/register", userRegisterDto);
        }
        public async Task GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/user/list");
            if (response != null)
            {
                ItemList = response;
            }
        }
        public async Task UpdateAsync(UserUpdateDto entity)
        {
            await _httpClient.PutAsJsonAsync("api/user/update", entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/user/delete/{id}");
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/user/info/{id}");
            if (result.StatusCode == HttpStatusCode.OK)
                return await result.Content.ReadFromJsonAsync<UserDto>();
            else
                return null;
        }
    }
}
