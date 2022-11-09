namespace Asset.Infrastructure.Repositories
{
    using Asset.Application.Persistence;
    using Asset.Domain.Entities;

    public class RelocationRepository : RepositoryBase<RelocationRequest>, IRelocationRepository
    {
        public RelocationRepository(AssetContext context)
            : base(context)
        {
        }
    }
}
