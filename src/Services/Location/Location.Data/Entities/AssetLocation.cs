namespace Location.Data.Entities
{
    public class AssetLocation
    {
        public int AssetId { get; set; }
        public int LocationId { get; set; }
        public DateTime dateTime { get; set; }
        public int UpdatedBy { get; set; }

        public Location? Location { get; set; }
    }
}
