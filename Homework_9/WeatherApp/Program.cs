using WeatherApp.Service;

var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddHttpClient<WeatherService>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/weather-forecast", async (string city, string countryCode, WeatherService weatherService) =>
        {
            if (string.IsNullOrEmpty(city))
            {
                return Results.BadRequest("Please provide a city name.");
            }

            if (string.IsNullOrEmpty(countryCode))
            {
                return Results.BadRequest("Please provide a city name.");
            }

            try
            {
                var weatherData = await weatherService.GetWeatherAsync(city, countryCode);

                return Results.Ok(weatherData);
            }
            catch (HttpRequestException e)
            {
                return Results.Problem($"Error fetching weather data: {e.Message}");
            }
        })
        .WithName("GetWeatherForecast")
        .WithTags("Weather")
        .WithOpenApi();

    app.Run();