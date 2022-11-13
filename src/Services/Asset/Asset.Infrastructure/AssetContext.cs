namespace Asset.Infrastructure
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AssetContext : DbContext
    {
        public AssetContext(DbContextOptions<AssetContext> options)
            : base(options)
        {
        }

        public DbSet<Asset> Asset { get; set; }
        public DbSet<RelocationRequest> RelocationRequest { get; set; }
        public DbSet<RenovationRequest> RenovationRequest { get; set; }
    }
}
