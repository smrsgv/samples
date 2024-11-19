var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("weather", client =>
{
    client.BaseAddress = new Uri("http://localhost:6000");
});

var app = builder.Build();

app.MapGet("/weatherforecast", async (IHttpClientFactory factory) =>
{
    using HttpClient client = factory.CreateClient("weather");

    var forecast =  await client.GetFromJsonAsync<WeatherForecast[]>("weatherforecast");
    
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
