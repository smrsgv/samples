var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDiscovery();

// configure service discovery for all http clients
// builder.Services.ConfigureHttpClientDefaults(options =>
// {
//     options.AddServiceDiscovery();
// });
// or configure service discovery for particular one
builder.Services.AddHttpClient("secondapi", client =>
{
    client.BaseAddress = new Uri("https+http://second");
}).AddServiceDiscovery();

var app = builder.Build();

app.MapGet("/weatherforecast", async (IHttpClientFactory factory) =>
{
    using HttpClient client = factory.CreateClient("secondapi");

    var res = await client.GetStringAsync("weatherforecast");

    return res;
});

app.Run();