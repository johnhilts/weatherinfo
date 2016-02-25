using System;

namespace Data.Model
{
    public class UserLocationDataModel
    {
        public Guid UserId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string InputName { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public byte SortOrder { get; set; }
    }
}
