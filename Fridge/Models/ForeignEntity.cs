using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models {
    public class ForeignEntity {
        public ForeignEntity()
        {
            PersonRepresentsForeignEntities = new HashSet<PersonRepresentsForeignEntity>();
            ForeignEntitySubscriptions = new HashSet<ForeignEntitySubscription>();
        }

        public int ForeignEntityId { get; set; }
        public string CountryCode { get; set; }
        public string CompanyReference { get; set; }
        public string ForeignEntityName { get; set; }

        public Country Country { get; set; }
        // nominees
        public ICollection<PersonRepresentsForeignEntity> PersonRepresentsForeignEntities { get; set; }
        // Foreign entitity subscriptions
        public ICollection<ForeignEntitySubscription> ForeignEntitySubscriptions { get; set; }
    }
}