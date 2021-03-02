using System.Collections.Generic;

namespace Fridge.Models {
    public class ShareClause {
        public ShareClause()
        {
            PersonSubscriptions = new HashSet<PersonSubscription>();
            ForeignEntitySubscriptions = new HashSet<ForeignEntitySubscription>();
            PrivateEntitySubscriptions = new HashSet<PrivateEntitySubscription>();
        }

        public int ShareClauseId { get; set; }
        public string Title { get; set; }
        public double NominalValue { get; set; }
        public int TotalNumberOfShares { get; set; }
        public int MemorandumId { get; set; }

        public MemorandumOfAssociation MemorandumOfAssociation { get; set; }

        // subscriptions by nominees
        public ICollection<PersonSubscription> PersonSubscriptions { get; set; }

        // subscriptions by foreign entities
        public ICollection<ForeignEntitySubscription> ForeignEntitySubscriptions { get; set; }

        // subscriptions by private entities
        public ICollection<PrivateEntitySubscription> PrivateEntitySubscriptions { get; set; }
    }
}