namespace Asset.Domain.Entities
{
    using Domain.Enums;

    public class Asset
    {
        public Asset()
        {
            this.RelocationRequests = new HashSet<RelocationRequest>();
        }

        public int Id { get; set; }
        public int InventoryNumber { get; set; }
        public int GuarantyMounts { get; set; }
        public int LocationId { get; set; }
        public string Producer { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int MyProperty { get; set; }
        public AssetType Type { get; set; }
        public AssetPeriodType PeriodType { get; set; }
        public AssetStatus Status { get; set; }

        public IEnumerable<RelocationRequest> RelocationRequests { get; set; }
    }
}
