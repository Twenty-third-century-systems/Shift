﻿using System;
using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewShareHolderRequestDto {
        public NewShareHolderRequestDto()
        {
            PeopleRepresented = new List<NewShareHolderRequestDto>();
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

        public List<NewShareHolderRequestDto> PeopleRepresented { get; set; }
        public List<ShareholderSubscriptionDto> Subs { get; set; }

        // public List<NewShareHoldingEntityRequestDto> RepresentedEntities { get; set; }

        public bool HasBeneficiaries()
        {
            return PeopleRepresented.Count > 0;
        }

        public bool HasSubscription()
        {
            return Subs.Count > 0;
        }
    }
}