namespace WeatherInfo.Application.Models.Location
{
    public class LocationInputModel
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
    }
}
