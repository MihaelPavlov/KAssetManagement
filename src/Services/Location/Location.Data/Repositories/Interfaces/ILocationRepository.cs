namespace Location.Data.Repositories.Interfaces
{
    using Location.Data.DTO;

    public interface ILocationRepository
    {
        Task<GetLocationById?> GetById(int locationId);
    }
}
