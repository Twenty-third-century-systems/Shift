using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class PrivateEntityHasSubscriber
    {
        public int PrivateEntityId { get; set; }
        public int SubscriberId { get; set; }
        public int RolesId { get; set; }
        public int SubscriptionId { get; set; }

        public PrivateEntity PrivateEntity { get; set; }
        public PrivateEntityRoles RolesInPrivateEntityRoles { get; set; }
        public PrivateEntitySubscriber Subscriber { get; set; }
        public PrivateEntitySubscription PrivateEntitySubscription { get; set; }
    }
}
