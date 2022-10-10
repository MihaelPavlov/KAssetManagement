namespace Location.Data
{
    using FluentMigrator.Runner;
    using FluentMigrator.Runner.Initialization;
    using Location.Data.Migrations;
    using Location.Data.Repositories;
    using Location.Data.Repositories.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class DI
    {
        public static void AddDb(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ILocationConnectionContext, LocationConnectionContext>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            var serviceProvider = DI.CreateServices(connectionString);
            using (var scope = serviceProvider.CreateScope())
            {
                DI.UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .Configure<AssemblySourceOptions>(x => x.AssemblyNames = new[] { typeof(_InitialCreate).Assembly.GetName().Name })
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(_InitialCreate).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .Configure<FluentMigratorLoggerOptions>(options =>
                {
                    options.ShowSql = true;
                    options.ShowElapsedTime = true;
                })
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
