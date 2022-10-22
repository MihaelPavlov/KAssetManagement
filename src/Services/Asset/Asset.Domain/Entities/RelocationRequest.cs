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

        public RequestStatus Status { get; set; }
        public RequestStatus GetRequest { get; set; }
        public RequestStatus Received { get; set; }

        public Asset? Asset { get; set; }
    }
}
