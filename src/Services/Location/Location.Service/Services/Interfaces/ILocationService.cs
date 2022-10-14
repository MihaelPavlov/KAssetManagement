namespace Location.Service.Services.Interfaces
{
    using Location.Service.DTO;

    public interface ILocationService
    {
        Task<Location> GetById(int id);
        Task<GetAllLocationsByOrganizationId> GetAllByOrganizationId(int organizationId);
        Task<int> Create(CreateLocationRequest request);
        Task Update(UpdateLocationRequest request);
        Task Delete(int id);
    }
}
