namespace Asset.Domain.Entities
{
    using Domain.Common;
    using Domain.Enums;

    public class Asset : EntityBase
    {
        public Asset()
        {
            this.RelocationRequests = new HashSet<RelocationRequest>();
        }

        public int InventoryNumber { get; set; }
        public int GuarantyMounts { get; set; }
        public int LocationId { get; set; }
        public string? Producer { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string Price { get; set; }
        public AssetType Type { get; set; }
        public AssetPeriodType PeriodType { get; set; }
        public AssetStatus Status { get; set; }

        public IEnumerable<RelocationRequest> RelocationRequests { get; set; }
    }
}
