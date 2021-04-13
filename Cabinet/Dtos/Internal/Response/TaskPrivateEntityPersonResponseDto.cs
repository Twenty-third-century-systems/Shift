using System;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskPrivateEntityPersonResponseDto {        
        public string FullName { get; set; }        
        public string Country { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalIdentification { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? DateOfAppointment { get; set; }
        public string Occupation { get; set; }
        public DateTime DateOfTakeUp { get; set; }
    }
}