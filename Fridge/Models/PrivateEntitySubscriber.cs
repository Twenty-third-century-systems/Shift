using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class PrivateEntitySubscriber
    {
        public PrivateEntitySubscriber()
        {
            PrivateEntityHasSubscribers = new HashSet<PrivateEntityHasSubscriber>();
        }

        public int PrivateEntitySubscriberId { get; set; }
        public string CountryCode { get; set; }
        public string NationalId { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public int GenderId { get; set; }
        public string PhysicalAddress { get; set; }
        public bool IsARepresentative { get; set; }
        // TODO: this relationship should be satisfied for other types of entities
        public int? PrivateEntityId { get; set; }
        public int? ForeignEntityId { get; set; }
        public Gender Gender { get; set; }
        public PrivateEntity PrivateEntity { get; set; }
        public ForeignEntity ForeignEntity { get; set; }
        public ICollection<PrivateEntityHasSubscriber> PrivateEntityHasSubscribers { get; set; }
    }
}
