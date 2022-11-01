namespace Asset.EventBus.Messages.Events
{
    using Asset.EventBus.Messages.Common;

    public class AssetCreateLocationEvent : IntegrationBaseEvent
    {
        public AssetCreateLocationEvent(int assetId, int locationId, int updatedBy)
        {
            this.AssetId = assetId;
            this.LocationId = locationId;
            this.UpdatedBy = updatedBy;
        }

        public int AssetId { get; set; }
        public int LocationId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
