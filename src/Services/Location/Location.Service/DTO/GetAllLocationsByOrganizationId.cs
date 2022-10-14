namespace Location.Service.DTO
{
    public class GetAllLocationsByOrganizationId
    {
        public GetAllLocationsByOrganizationId()
        {
            this.Locations = new HashSet<Location>();
        }

        public int TotalRecords { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}
