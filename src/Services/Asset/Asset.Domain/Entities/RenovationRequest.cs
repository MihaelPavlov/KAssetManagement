namespace Asset.Domain.Entities
{
    using Domain.Common;

    public class RenovationRequest : EntityBase
    {
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProblemMessage { get; set; } = string.Empty;
        public int Status { get; set; }
        public int? IsItGiven { get; set; }
        public int? IsItRenovated { get; set; }

        public Asset? Asset { get; set; }

    }
}
