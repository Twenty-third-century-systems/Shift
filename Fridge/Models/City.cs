using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string District { get; set; }
        public bool CanSort { get; set; }
        
        public Country Country { get; set; }
        public ICollection<ServiceApplication> Applications { get; set; }
        public ICollection<PrivateEntityOffice> EntityOffices { get; set; }
    }
}
