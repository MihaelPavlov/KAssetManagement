namespace Location.Service
{
    using Location.Data;
    using Location.Service.Profiles;
    using Location.Service.Services;
    using Location.Service.Services.Interfaces;
    using Location.Service.UnitOfWork;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DI
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDb(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddAutoMapper(typeof(ServicesProfile));
        }
    }
}
