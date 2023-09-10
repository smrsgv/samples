using Microsoft.EntityFrameworkCore;
using GrpcSample.Server.Data;
using GrpcSample.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=Data/todoapp.db"));

builder.Services.AddGrpc().AddJsonTranscoding();

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<ToDoService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();