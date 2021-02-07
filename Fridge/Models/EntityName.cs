using System;
using System.Collections.Generic;

#nullable disable

namespace Fridge.Models
{
    public class EntityName
    {
        public EntityName()
        {
            PvtEntities = new HashSet<PrivateEntity>();
        }

        public int EntityNameId { get; set; }
        public string Value { get; set; }
        public int StatusId { get; set; }
        public int NameSearchId { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }
        public NameSearch NameSearch { get; set; }
        public ICollection<PrivateEntity> PvtEntities { get; set; }
    }
}
