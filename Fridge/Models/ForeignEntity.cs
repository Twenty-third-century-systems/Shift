using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class ForeignEntity
    {
        public ForeignEntity()
        {
            PvtEntityHasForeignEntities = new HashSet<PrivateEntityHasForeignEntity>();
        }

        public int ForeignEntityId { get; set; }
        public string CountryCode { get; set; }
        public string CompanyReference { get; set; }
        public string ForeignEntityName { get; set; }

        public Country Country { get; set; }
        public ICollection<PrivateEntityHasForeignEntity> PvtEntityHasForeignEntities { get; set; }
    }
}
