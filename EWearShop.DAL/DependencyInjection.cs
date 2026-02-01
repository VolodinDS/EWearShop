using EWearShop.DAL.Seed;
using EWearShop.Domain.Products;
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
            services.AddScoped<IEWearShopDbContext, EWearShopDbContext>();
            services.AddSingleton(TimeProvider.System);
            services.AddSingleton<ProductFactory>(provider => new ProductFactory(provider.GetRequiredService<TimeProvider>()));

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
            var productFactory = scope.ServiceProvider.GetRequiredService<ProductFactory>();
            var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

            dbContext.Database.Migrate();

            ProductsSeed.Seed(dbContext, productFactory, timeProvider);

            return app;
        }
    }
}