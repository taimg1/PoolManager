using PoolMS.Service.DTO;
using System.Net.Http.Json;

namespace PoolMS.UI.WebAssembly.Service
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<WeatherReport> WeatherReport { get; set; }= new List<WeatherReport>();
        public async Task GetWeatherReport()
        {
            var result = await _httpClient.GetFromJsonAsync<List<WeatherReport>>("api/weatherapi/getweather");
            if (result != null)
            {
                WeatherReport = result;
            }
        }
    }
}
