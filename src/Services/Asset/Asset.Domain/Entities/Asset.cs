namespace Asset.Domain.Entities
{
    using Domain.Common;

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
        public string? Price { get; set; }
        public int Type { get; set; }
        public int PeriodType { get; set; }
        public int Status { get; set; }

        public IEnumerable<RelocationRequest> RelocationRequests { get; set; }
    }
}
