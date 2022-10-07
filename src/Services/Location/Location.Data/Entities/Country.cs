namespace Location.Data.Entities
{
    public class Country
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
