namespace Asset.EventBus.Messages.Events
{
    using Asset.EventBus.Messages.Common;

    public class AssetCreateLocationEvent : IntegrationBaseEvent
    {
        public int AssetId { get; set; }
        public int LocationId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
