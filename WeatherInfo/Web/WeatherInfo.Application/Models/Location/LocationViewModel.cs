namespace WeatherInfo.Application.Models.Location
{
    public class LocationInputModel
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string InputName { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
    }
}
