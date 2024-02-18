using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PoolMS.Service.DTO;
using System.Text.Json;

namespace PoolMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherAPIController : ControllerBase
    {
        private const string apiKey = "ff81c6940bc73ffe42d72235aaad4c8f";
        private const string city = "Kyiv";
        private const string  url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric";
        private readonly HttpClient _client;
        public WeatherAPIController(HttpClient client)
        {
            _client = client;
        }
        [HttpGet("GetWeather")]
        public async Task<IActionResult> GetWeather()
        {
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(content);
                var listElement = doc.RootElement.GetProperty("list");

                var weatherReports = new List<WeatherReport>();

                foreach (var item in listElement.EnumerateArray())
                {
                    var weatherReport = new WeatherReport
                    {
                        Date = DateTime.Parse(item.GetProperty("dt_txt").GetString()),
                        LowTemperature = item.GetProperty("main").GetProperty("temp_min").GetDouble(),
                        HighTemperature = item.GetProperty("main").GetProperty("temp_max").GetDouble(),
                        Summary = item.GetProperty("weather")[0].GetProperty("description").GetString(),
                        WindSpeed = item.GetProperty("wind").GetProperty("speed").GetDouble()
                    };

                    weatherReports.Add(weatherReport);
                }

                return Ok(weatherReports);
            }

            return BadRequest();
        }
    }
  
}
