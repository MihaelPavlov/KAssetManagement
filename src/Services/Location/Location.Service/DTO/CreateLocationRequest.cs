namespace Location.Service.DTO
{
    public class CreateLocationRequest
    {
        public string? Code { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string? Street { get; set; }
        public int StreetNumber { get; set; }
    }
}
