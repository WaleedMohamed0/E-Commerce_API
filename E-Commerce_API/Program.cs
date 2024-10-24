using E_Commerce_API.Helper;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependency(builder.Configuration);

var app = builder.Build();

await app.ConfigureMiddleWaresAsync();

app.Run();
