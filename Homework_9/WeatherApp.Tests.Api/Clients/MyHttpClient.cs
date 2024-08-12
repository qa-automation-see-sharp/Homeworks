using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Tests.Api.Clients;

public class MyHttpClient
{
    private readonly HttpClient _client;

    public MyHttpClient()
    {
        _client = new HttpClient();
    }

    public void ConfigureClient(string baseUrl)
    {
        _client.BaseAddress = new Uri(baseUrl);
    }

    public async Task<WeatherInfo?> GetWeather(string city, string countryCode)
    {
        var uri = $"weather-forecast?city={city}&countryCode={countryCode}";
        var response = await _client.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(JsonConvert.SerializeObject(jsonString, Formatting.Indented));
        var tmp = JsonConvert.DeserializeObject<WeatherInfo>(jsonString);
        return tmp;
    }
}