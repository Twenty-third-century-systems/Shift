using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class PrivateEntityOffice
    {
        public PrivateEntityOffice()
        {
            PrivateEntities = new HashSet<PrivateEntity>();
        }

        public int PrivateEntityOfficeId { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
        public int CityId { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public City City { get; set; }
        public ICollection<PrivateEntity> PrivateEntities { get; set; }
    }
}
