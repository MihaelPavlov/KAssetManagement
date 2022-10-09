namespace Location.Data
{
    using Microsoft.Extensions.Configuration;
    using Npgsql;
    using System.Data;
    using System.Data.Common;

    public class LocationConnectionContext : ILocationConnectionContext
    {
        private readonly IConfiguration configuration;

        public LocationConnectionContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<IDbConnection> CreateConnection()
        {
            return Task.FromResult((IDbConnection)new NpgsqlConnection(this.GetConnectionString()));
        }

        public string GetConnectionString()
        {
            var connectionString = this.configuration.GetValue<string>("DatabaseSettings:ConnectionString");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("The connection stirng inside the Location.Data project is null!");
            }

            return connectionString;
        }
    }
}
