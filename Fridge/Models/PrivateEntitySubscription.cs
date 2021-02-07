using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class PrivateEntitySubscription
    {
        public PrivateEntitySubscription()
        {
            PrivateEntityHasForeignEntities = new HashSet<PrivateEntityHasForeignEntity>();
            PrivateEntityHasPvtEntities = new HashSet<PrivateEntityHasPrivateEntity>();
            PrivateEntityHasSubscribers = new HashSet<PrivateEntityHasSubscriber>();
        }

        public int PrivateEntitySubscriptionId { get; set; }
        public long? OrdinaryShares { get; set; }
        public long? PreferenceShares { get; set; }

        public ICollection<PrivateEntityHasForeignEntity> PrivateEntityHasForeignEntities { get; set; }
        public ICollection<PrivateEntityHasPrivateEntity> PrivateEntityHasPvtEntities { get; set; }
        public ICollection<PrivateEntityHasSubscriber> PrivateEntityHasSubscribers { get; set; }
    }
}
