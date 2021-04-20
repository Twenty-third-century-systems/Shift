using System;
using System.Collections.Generic;

// director member
namespace Fridge.Models.Main {
    public class ShareHolder : Person {
        public ShareHolder()
        {
            PersonSubscriptions = new HashSet<PersonSubscription>();
            PersonHoldsSharesInPrivateEntities = new HashSet<PersonHoldsSharesInPrivateEntity>();
            PersonRepresentsForeignEntities = new HashSet<PersonRepresentsForeignEntity>();
            PersonRepresentsPersons = new HashSet<PersonRepresentsPerson>();
            PersonRepresentsPersonss = new HashSet<PersonRepresentsPerson>();
            PersonRepresentsPrivateEntities = new HashSet<PersonRepresentsPrivateEntity>();
        }
        
        public string Occupation { get; set; }
        public DateTime? DateOfTakeUp { get; set; }

        //subscriptions
        public ICollection<PersonSubscription> PersonSubscriptions { get; set; }

        // entities subscribed
        public ICollection<PersonHoldsSharesInPrivateEntity> PersonHoldsSharesInPrivateEntities { get; set; }

        // Beneficiary foreign entities
        public ICollection<PersonRepresentsForeignEntity> PersonRepresentsForeignEntities { get; set; }

        // Beneficiary people
        public ICollection<PersonRepresentsPerson> PersonRepresentsPersons { get; set; }

        //people  Nominees
        public ICollection<PersonRepresentsPerson> PersonRepresentsPersonss { get; set; }

        // Beneficiary Private Entities
        public ICollection<PersonRepresentsPrivateEntity> PersonRepresentsPrivateEntities { get; set; }
    }
}