using System.Collections.Generic;

namespace Cabinet.Dtos.External.Request {
    public class NewShareHoldingEntityRequestDto {
        public NewShareHoldingEntityRequestDto()
        {
            Nominees = new List<NewShareHolderRequestDto>();
            Subs = new List<ShareholderSubscriptionDto>();
        }
        public string CountryCode { get; set; }
        public string CompanyReference { get; set; }
        public string Name { get; set; }

        public List<NewShareHolderRequestDto> Nominees { get; set; }
        public List<ShareholderSubscriptionDto> Subs { get; set; }

        public bool IsRepresented()
        {
            return Nominees.Count > 0;
        }
    }
}