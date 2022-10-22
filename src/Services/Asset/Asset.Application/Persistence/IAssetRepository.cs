namespace Asset.Application.Persistence
{
    using Asset.Domain.Entities;

    public interface IAssetRepository : IAsyncRepository<Asset>
    {
    }
}
