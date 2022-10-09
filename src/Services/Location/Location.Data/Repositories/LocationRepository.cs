namespace Location.Data.Repositories
{
    using Dapper;
    using Location.Data.DTO;
    using Location.Data.Repositories.Interfaces;

    public class LocationRepository : ILocationRepository
    {
        private readonly ILocationConnectionContext connectionContext;

        public LocationRepository(ILocationConnectionContext connectionContext)
        {
            this.connectionContext = connectionContext;
        }

        public async Task<GetLocationById?> GetById(int locationId)
        {
            using (var connection = await this.connectionContext.CreateConnection())
            {
                var execProc = @"SELECT * FROM get_location_by_id (@location_id);";
                var parameters = new DynamicParameters();
                parameters.Add("@location_id", locationId);
                return (await connection.QueryAsync<GetLocationById>(execProc, parameters, commandType: System.Data.CommandType.Text)).SingleOrDefault();
            }
        }

    }
}
