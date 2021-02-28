using System;
using System.Collections.Generic;

namespace Cabinet.Dtos.Internal.Response {
    public class TaskPrivateEntityShareHolderResponseDto {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalIdentification { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? DateOfAppointment { get; set; }
        public bool IsSecretary { get; set; }
        public bool IsDirector { get; set; }
        public string Occupation { get; set; }
        public DateTime DateOfTakeUp { get; set; }

        public List<TaskPrivateEntityShareHolderResponseDto> Nominees { get; set; }
        public List<TaskPrivateEntityShareholderSubscriptionResponseDto> Subscriptions { get; set; }
    }
}