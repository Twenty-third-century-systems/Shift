using System;
using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewShareHolderRequestDto : NewPersonRequestDto {
        public NewShareHolderRequestDto()
        {
            PeopleRepresented = new List<NewShareHolderRequestDto>();
        }
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