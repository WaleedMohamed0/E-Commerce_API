using E_Commerce.Repository.Data;
using E_Commerce.Repository.Data.Contexts;
using E_Commerce_API.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Helper
{
    public static class ConfigureMiddleWares
    {
        public static async Task<WebApplication> ConfigureMiddleWaresAsync(this WebApplication app)
        {
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
            // To Handle Server Error Status Code (500)
            app.UseMiddleware<ExceptionMiddleWare>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // To Handle when a user tries to access a route that does not exist (Not Found Error)
            app.UseStatusCodePagesWithReExecute("/not-found");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            return app;
        }
    }
}
