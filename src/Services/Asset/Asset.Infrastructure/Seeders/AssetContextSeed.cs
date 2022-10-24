namespace Asset.Infrastructure.Seeders
{
    using Microsoft.Extensions.Logging;

    public class AssetContextSeed
    {
        public static async Task SeedAsync(AssetContext assetContext, ILogger<AssetContextSeed> logger)
        {
            if (!assetContext.Asset.Any())
            {
                assetContext.Asset.AddRange(GetPreconfiguredOrders());
                await assetContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(AssetContext).Name);
            }
        }

        private static IEnumerable<Domain.Entities.Asset> GetPreconfiguredOrders()
        {
            return new List<Domain.Entities.Asset>
            {
                new Domain.Entities.Asset()
                {
                    InventoryNumber= 1,
                    GuarantyMounts = 6,
                    LocationId=1,
                    Producer= "Tesla",
                    Brand = "Cars",
                    Model= "2022 Tesla Car",
                    Price ="200 000",
                    Type = 0,
                    PeriodType = 1,
                    Status = 0
                }
            };
        }
    }
}
