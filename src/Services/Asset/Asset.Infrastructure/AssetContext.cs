namespace Asset.Infrastructure
{
    using Microsoft.EntityFrameworkCore;

    public class AssetContext : DbContext
    {
        public AssetContext(DbContextOptions<AssetContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Entities.Asset> Asset { get; set; }
    }
}
