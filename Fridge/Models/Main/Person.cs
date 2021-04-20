using System;
using Fridge.Constants;

namespace Fridge.Models.Main {
    public class Person {
        public int PersonId { get; set; }
        public string CountryCode { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public EGender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalIdentification { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }        


        public Country Country { get; set; }
    }
}