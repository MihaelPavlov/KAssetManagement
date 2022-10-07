using FluentMigrator.Runner;
using Location.Data.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Location.Data
{
    public static class DI
    {
        public static void AddDb(this IServiceCollection services, string connectionString)
        {
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
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(CreateLocationTable).Assembly, typeof(CreateTestMigration).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
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
