namespace EventBus.Messages.AssetEvents
{
    using EventBus.Messages.Common;

    public class CreateAssetLocationEvent : IntegrationBaseEvent
    {
        public int AssetId { get; set; }
        public int LocationId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
