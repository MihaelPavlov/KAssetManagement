namespace Location.Data.Repositories
{
    using Dapper;
    using Location.Data.DTO;
    using Location.Data.Entities;
    using Location.Data.Repositories.Interfaces;
    using System.Data;

    public class LocationRepository : ILocationRepository
    {
        private readonly ILocationConnectionContext connectionContext;

        public LocationRepository(ILocationConnectionContext connectionContext)
        {
            this.connectionContext = connectionContext;
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

        public async Task<int> CreateLocation(CreateLocation request)
        {
            using (var connection = await this.connectionContext.CreateConnection())
            {
                var execFunction = @"SELECT * FROM create_location (@code,@latitude,@longitude,@country_id,@city_id,@street,@street_number,@organization_id,@created_by)";

                var parameters = new DynamicParameters();
                parameters.Add("code", request.Code, DbType.String, ParameterDirection.Input);
                parameters.Add("latitude", request.Latitude, DbType.String, ParameterDirection.Input);
                parameters.Add("longitude", request.Longitude, DbType.String, ParameterDirection.Input);
                parameters.Add("country_id", request.CountryId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("city_id", request.CityId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("street", request.Street, DbType.String, ParameterDirection.Input);
                parameters.Add("street_number", request.StreetNumber, DbType.Int32, ParameterDirection.Input);
                parameters.Add("organization_id", request.OrganizationId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("created_by", request.UpdatedBy, DbType.Int32, ParameterDirection.Input);
                parameters.Add("f_id", dbType: DbType.Int32, direction: ParameterDirection.Output);


                await connection.ExecuteAsync(execFunction, parameters, commandType: CommandType.Text);
                return parameters.Get<int>("f_id");
            }
        }

        public async Task UpdateLocation(UpdateLocation request)
        {
            using (var connection = await this.connectionContext.CreateConnection())
            {
                var execFunction = @"SELECT update_location (@location_id, @code,@latitude,@longitude,@country_id,@city_id,@street,@street_number,@organization_id,@updated_by)";

                var parameters = new DynamicParameters();
                parameters.Add("location_id", request.LocationId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("code", request.Code, DbType.String, ParameterDirection.Input);
                parameters.Add("latitude", request.Latitude, DbType.String, ParameterDirection.Input);
                parameters.Add("longitude", request.Longitude, DbType.String, ParameterDirection.Input);
                parameters.Add("country_id", request.CountryId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("city_id", request.CityId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("street", request.Street, DbType.String, ParameterDirection.Input);
                parameters.Add("street_number", request.StreetNumber, DbType.Int32, ParameterDirection.Input);
                parameters.Add("organization_id", request.OrganizationId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("updated_by", request.UpdatedBy, DbType.Int32, ParameterDirection.Input);

                var affectedRows = await connection.ExecuteAsync(execFunction, parameters, commandType: CommandType.Text);

                // TODO: if (affectedRows == 0)
                //    throw new NotFoundException("Location", request.LocaionId);
            }
        }

        public async Task DeleteLocation(DeleteLocation request)
        {
            using (var connection = await this.connectionContext.CreateConnection())
            {
                var execFunction = @"SELECT delete_location (@location_id)";

                var parameters = new DynamicParameters();
                parameters.Add("location_id", request.LocationId, DbType.Int32, ParameterDirection.Input);

                await connection.ExecuteAsync(execFunction, parameters, commandType: CommandType.Text);
            }
        }

        public async Task CreateAssetLocation(AssetLocation request)
        {
            using (var connection = await this.connectionContext.CreateConnection())
            {
                var execFunction = @"SELECT create_asset_location (@asset_id, @location_id, @creation_date, @updated_by)";
                var parameters = new DynamicParameters();
                parameters.Add("location_id", request.LocationId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("asset_id", request.AssetId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("creation_date", request.CreationDate, DbType.Date, ParameterDirection.Input);
                parameters.Add("updated_by", request.LocationId, DbType.Int32, ParameterDirection.Input);

                await connection.ExecuteAsync(execFunction, parameters, commandType: CommandType.Text);
            }
        }
    }
}
