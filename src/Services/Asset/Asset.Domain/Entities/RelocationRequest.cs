namespace Asset.Domain.Entities
{
    using Domain.Enums;

    public class RelocationRequest
    {
        public int Id { get; set; }
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
