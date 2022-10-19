namespace Asset.Infrastructure
{
    using Microsoft.EntityFrameworkCore;

    public class AssetContext : DbContext
    {
        public AssetContext(DbContextOptions<AssetContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=(local); database=KAS.Database.Asset; Integrated Security=true");
            }
#endif
        }
    }
}
