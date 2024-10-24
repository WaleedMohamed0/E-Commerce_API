using E_Commerce.Repository.Data.Contexts;
using E_Commerce.Repository.Data.Repos;
using E_Commerce.Service.Services.BrandsAndTypes;
using E_Commerce.Service.Services.Products;
using E_Commerce_API.Errors;
using E_Commerce_API.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwagger();
            services.AddDbContext(configuration);
            services.AddUserDefinedServices();
            services.AddAutoMapper(configuration);
            services.HandleValidationError();
            return services;
        }
        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("E-Commerce.Repository"));
            });
            return services;
        }
        private static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandTypeService, BrandTypeService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
        private static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new BrandsAndTypesProfile(configuration)));
            return services;
        }
        private static IServiceCollection HandleValidationError(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
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
            return services;
        }
    }
}
