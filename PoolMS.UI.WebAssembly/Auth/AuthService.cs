using Blazored.LocalStorage;
using PoolMS.Service.DTO;
using System.Net.Http.Json;

namespace PoolMS.UI.WebAssembly.Auth
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<bool> Login(UserLoginDto userLoginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/login", userLoginDto);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

            await _localStorage.SetItemAsync("jwt", tokenResponse.Token);

            return true;
        }
    }
    public class TokenResponse
    {
        public string Token { get; set; }
    }

}
