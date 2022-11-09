namespace Asset.Infrastructure
{
    using Asset.Application.Persistence;
    using Asset.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DI
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AssetContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AssetConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IRelocationRepository, RelocationRepository>();
            return services;
        }
    }
}
