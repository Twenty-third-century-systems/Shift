using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class PrivateEntityHasForeignEntity
    {
        public int PrivateEntityId { get; set; }
        public int ForeignEntityId { get; set; }
        public int SubscriptionId { get; set; }

        public ForeignEntity ForeignEntityNavigation { get; set; }
        public PrivateEntity PrivateEntity { get; set; }
        public PrivateEntitySubscription PrivateEntitySubscription { get; set; }
    }
}
