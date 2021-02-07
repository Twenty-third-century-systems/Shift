using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class PrivateEntityHasPrivateEntity
    {
        public int OwnerId { get; set; }
        public int OwnsId { get; set; }
        public int SubscriptionId { get; set; }

        public PrivateEntity OwnsNavigation { get; set; }
        public PrivateEntity OwnerNavigation { get; set; }
        public PrivateEntitySubscription PrivateEntitySubscription { get; set; }
    }
}
