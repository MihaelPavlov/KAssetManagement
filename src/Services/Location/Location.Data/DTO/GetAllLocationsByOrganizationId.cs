namespace Location.Data.DTO
{
    public class GetAllLocationsByOrganizationId
    {
        public GetAllLocationsByOrganizationId()
        {
            this.Locations = new HashSet<LocationResultDTO>();
        }

        public int TotalRecords { get; set; }
        public ICollection<LocationResultDTO> Locations { get; set; }
    }
}
