namespace Location.Data.DTO
{
    public class DeleteLocation
    {
        public DeleteLocation(int locationId)
        {
            LocationId = locationId;
        }

        public int LocationId { get; set; }
    }
}
