namespace Asset.Infrastructure.Repositories
{
    using Asset.Application.Persistence;
    using Asset.Domain.Entities;

    public class RenovationRepository : RepositoryBase<RenovationRequest>, IRenovationRepository
    {
        public RenovationRepository(AssetContext context)
            : base(context)
        {
        }
    }
}
