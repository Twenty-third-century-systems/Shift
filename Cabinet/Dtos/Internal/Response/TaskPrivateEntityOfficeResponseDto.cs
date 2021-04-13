using System;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskPrivateEntityOfficeResponseDto {        
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
        public string CityTown { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime EffectiveFrom { get; set; }
    }
}