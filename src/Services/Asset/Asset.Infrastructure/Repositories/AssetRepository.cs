namespace Asset.Infrastructure.Repositories
{
    using Asset.Application.Persistence;

    public class AssetRepository : RepositoryBase<Domain.Entities.Asset>, IAssetRepository
    {
        public AssetRepository(AssetContext context)
            : base(context)
        {

        }
    }
}
