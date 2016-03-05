namespace Location.Model
{
    public class Address
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string InputName { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public int SortOrder { get; set; }
    }
}
