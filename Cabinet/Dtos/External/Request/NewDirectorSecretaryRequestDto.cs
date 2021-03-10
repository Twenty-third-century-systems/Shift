using System;

namespace Cabinet.Dtos.External.Request {
    public class NewDirectorSecretaryRequestDto : NewPersonRequestDto {
        public DateTime DateOfAppointment { get; set; }
    }
}