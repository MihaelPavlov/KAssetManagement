namespace Location.Data.Repositories
{
    using Dapper;
    using Location.Data.DTO;
    using Location.Data.Repositories.Interfaces;
    using System.Data;

    public class LocationRepository : ILocationRepository
    {
        private readonly ILocationConnectionContext connectionContext;

        public LocationRepository(ILocationConnectionContext connectionContext)
        {
            this.connectionContext = connectionContext;
        }

        public Task<int> CreateLocation(CreateLocation request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAllLocationsByOrganizationId> GetAllByOrganizationId(int organizationId)
        {
            using (var connection = await this.connectionContext.CreateConnection())
            {
                var result = new GetAllLocationsByOrganizationId();
                var execFunction = @"SELECT * FROM get_all_locations_by_organization_id (@organization_id);";

                var parameters = new DynamicParameters();
                parameters.Add("organization_id", organizationId, DbType.Int32, ParameterDirection.Input);

                using (var multiQuery = await connection.QueryMultipleAsync(execFunction, parameters, commandType: CommandType.Text))
                {
                    result.Locations = multiQuery.Read<LocationResultDTO>().AsList();
                    result.TotalRecords = result.Locations.Count();

                    return result;
                }
            }
        }

        public async Task<GetLocationById?> GetById(int locationId)
        {
            using (var connection = await this.connectionContext.CreateConnection())
            {
                var execFunction = @"SELECT * FROM get_location_by_id (@location_id);";

                var parameters = new DynamicParameters();
                parameters.Add("@location_id", locationId, DbType.Int32, ParameterDirection.Input);

                return (await connection.QueryAsync<GetLocationById>(execFunction, parameters, commandType: System.Data.CommandType.Text)).SingleOrDefault();
            }
        }

    }
}
