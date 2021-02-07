using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class PrivateEntityRoles
    {
        public PrivateEntityRoles()
        {
            PvtEntityHasSubscribers = new HashSet<PrivateEntityHasSubscriber>();
        }

        public int PrivateEntityRolesId { get; set; }
        public bool Director { get; set; }
        public bool Member { get; set; }
        public bool Secretary { get; set; }
        
        //TODO implement entity represantation here

        public ICollection<PrivateEntityHasSubscriber> PvtEntityHasSubscribers { get; set; }
    }
}
