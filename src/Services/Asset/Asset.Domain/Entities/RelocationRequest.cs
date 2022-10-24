namespace Asset.Domain.Entities
{
    using Domain.Common;
    using Domain.Enums;

    public class RelocationRequest : EntityBase
    {
        public int AssetId { get; set; }

        public int FromSiteId { get; set; }
        public int ToSiteId { get; set; }

        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }

        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }

        public int Status { get; set; } // RequestStatus
        public int GetRequest { get; set; } // RequestStatus
        public int Received { get; set; } // RequestStatus

        public Asset? Asset { get; set; }
    }
}
