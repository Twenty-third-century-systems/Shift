using System;

namespace Cabinet.Dtos.External.Request {
    public class NewPersonRequestDto {
        public string CountryCode { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalIdentification { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}