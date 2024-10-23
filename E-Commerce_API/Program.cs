using E_Commerce.Repository.Data;
using E_Commerce.Repository.Data.Repos;
using E_Commerce.Service.Services.Products;
using E_Commerce.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Service.Services.BrandsAndTypes;
using E_Commerce_API.Mapping;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_API.Errors;
using E_Commerce_API.Middlewares;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b=>b.MigrationsAssembly("E-Commerce.Repository"));
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandTypeService, BrandTypeService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(m=>m.AddProfile(new ProductProfile(builder.Configuration)));
builder.Services.AddAutoMapper(m => m.AddProfile(new BrandsAndTypesProfile(builder.Configuration)));
// To Handle Validation Error
builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            var errors = actionContext.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToArray();
            var errorResponse = new ApiValidationErrorResponse
            {
                Errors = errors
            };
            return new BadRequestObjectResult(errorResponse);
        };
    });

var app = builder.Build();
// Configure the HTTP request pipeline.
using var scope = app.Services.CreateScope();
// Get the instance of ECommerceDbContext in our services layer
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreDbContext>();
var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    // Automatically apply any pending migrations
    await context.Database.MigrateAsync();
    // Seed the database
    await LoadDataSeed.SeedData(context);
}
catch (Exception ex)
{
    var logger = LoggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occurred while migrating the database.");
}
// To Handle Server Error
app.UseMiddleware<ExceptionMiddleWare>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
