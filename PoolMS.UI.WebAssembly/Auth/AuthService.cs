using Blazored.LocalStorage;
using PoolMS.Service.DTO;
using System.IdentityModel.Tokens.Jwt;
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
        public async Task SetJwtTokenInHeader()
        {
            var token = await _localStorage.GetItemAsync<string>("jwt");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        public async Task CheckAndRemoveExpiredToken()
        {
            var token = await _localStorage.GetItemAsync<string>("jwt");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    await _localStorage.RemoveItemAsync("jwt");
                }
            }
        }
        public async Task<bool> IsUserAuthenticated()
        {
            var token = await _localStorage.GetItemAsync<string>("jwt");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                if (jwtToken.ValidTo > DateTime.UtcNow)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("jwt");
        }
    }
    public class TokenResponse
    {
        public string Token { get; set; }
    }

}
