using WeatherApp.Models;
using WeatherApp.Tests.Api.Clients;

namespace WeatherApp.Tests.Api;

public class Tests
{
    private MyHttpClient _client;
    [SetUp]
    public void Setup()
    {
        _client = new MyHttpClient();
        _client.ConfigureClient("http://localhost:5119/");
    }

    [Test]
    public async Task GetWeatherForLondon()
    {
        // Get weather for London, GB
        // use method GetWeather from MyHttpClient
        WeatherInfo? response = await _client.GetWeather("London", "GB");
        
        
        //Asserts should pass
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Name, Is.EqualTo("London"));
            Assert.That(response.Sys.Country, Is.EqualTo("GB"));
        });
    }
    
    [Test]
    public async Task GetWeatherForParis()
    {
        // Get weather for Paris, GB
        // use method GetWeather from MyHttpClient
        WeatherInfo? response = await _client.GetWeather("Paris", "FR");
        
        
        //Asserts should pass
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Name, Is.EqualTo("Paris"));
            Assert.That(response.Sys.Country, Is.EqualTo("FR"));
        });
    }
    
    [Test]
    public async Task GetWeatherForOdesaUkraine()
    {
        // Get weather for Odesa, UA
        // use method GetWeather from MyHttpClient
        WeatherInfo? response = await _client.GetWeather("Odesa", "UA");
        
        
        //Asserts should pass
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Name, Is.EqualTo("Odesa"));
            Assert.That(response.Sys.Country, Is.EqualTo("UA"));
        });
    }
    
    [Test]
    public async Task GetWeatherForOdessaAmerica()
    {
        // Get weather for Odessa, US
        // use method GetWeather from MyHttpClient
        WeatherInfo? response = await _client.GetWeather("Odessa", "US");
        
        
        //Asserts should pass
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Name, Is.EqualTo("Odessa"));
            Assert.That(response.Sys.Country, Is.EqualTo("US"));
        });
    }
}