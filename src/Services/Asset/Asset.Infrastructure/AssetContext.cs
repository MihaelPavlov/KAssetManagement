namespace Asset.Infrastructure
{
    using Asset.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AssetContext : DbContext
    {
        public AssetContext(DbContextOptions<AssetContext> options)
            : base(options)
        {
        }

        public DbSet<Asset> Asset { get; set; }
        public DbSet<RelocationRequest> RelocationRequest { get; set; }
    }
}
