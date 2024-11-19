var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/weatherforecast", () =>
{
    return "Second API Service Response";
});

app.Run();