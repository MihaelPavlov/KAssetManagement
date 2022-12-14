namespace Location.Data.DTO
{
    public class UpdateLocation
    {
        public int LocationId { get; set; }
        public string? Code { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string? Street { get; set; }
        public int StreetNumber { get; set; }
        public int OrganizationId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
