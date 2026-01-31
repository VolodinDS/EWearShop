using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EWearShop.DAL;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDataAccessLayer()
        {
            services.AddDbContext<EWearShopDbContext>(builder =>
            {
                string dbPath = Path.Combine(Constants.DatabaseFolder, Constants.DatabaseFileName);
                builder.UseSqlite($"Data Source={dbPath}");
            });

            return services;
        }
    }

    extension(IApplicationBuilder app)
    {
        public IApplicationBuilder UseDataAccessLayer()
        {
            Directory.CreateDirectory(Constants.DatabaseFolder);

            using IServiceScope scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<EWearShopDbContext>();

            dbContext.Database.Migrate();

            return app;
        }
    }
}