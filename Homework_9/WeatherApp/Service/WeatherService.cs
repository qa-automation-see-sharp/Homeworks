using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Service;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    //TODO: Replace with your own API key
    private const string ApiKey = "b49ee52fbea47d99652d7f990ff366f9";
    private const string WeatherEndpoint = "/data/2.5/weather";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.openweathermap.org");
    }
    
    public async Task<WeatherInfo?> GetWeatherAsync(string city, string countryConde)
    {
        var url = $"{WeatherEndpoint }?q={city},{countryConde}&appid={ApiKey}&units=metric";
        var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<WeatherInfo>(jsonString);
    }
}