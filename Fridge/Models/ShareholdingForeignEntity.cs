using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models {
    public class ShareholdingForeignEntity {
        public ShareholdingForeignEntity()
        {
            Representatives = new HashSet<ShareHoldingForeignEntityHasPrivateEntityOwner>();
            Subscriptions = new HashSet<ShareHoldingForeignEntityHasShareClause>();
        }

        public int ShareholdingForeignEntityId { get; set; }
        public string CountryCode { get; set; }
        public string CompanyReference { get; set; }
        public string ForeignEntityName { get; set; }

        public Country Country { get; set; }
        public ICollection<ShareHoldingForeignEntityHasPrivateEntityOwner> Representatives { get; set; }
        public ICollection<ShareHoldingForeignEntityHasShareClause> Subscriptions { get; set; }
    }
}