using System;
using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewShareHolderRequestDto {
        public NewShareHolderRequestDto()
        {
            Nominees = new List<NewShareHolderRequestDto>();
        }

        public string CountryCode { get; set; }
        public string Surname { get; set; }
        public string Names { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalIdentification { get; set; }
        public string PhysicalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsSecretary { get; set; }
        public bool IsDirector { get; set; }
        public string Occupation { get; set; }
        public DateTime? DateOfTakeUp { get; set; }

        public List<NewShareHolderRequestDto> Nominees { get; set; }
        public List<ShareholderSubscriptionDto> Subscriptions { get; set; }

        public bool HasNominees()
        {
            return Nominees.Count > 0;
        }
    }
}