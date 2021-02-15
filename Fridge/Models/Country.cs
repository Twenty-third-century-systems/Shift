using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Fridge.Models
{
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            ForeignEntities = new HashSet<ShareholdingForeignEntity>();
            PrivateOwners = new HashSet<PrivateEntityOwner>();
        }
        
        public string Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Region { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<ShareholdingForeignEntity> ForeignEntities { get; set; }
        public ICollection<PrivateEntityOwner> PrivateOwners { get; set; }
    }
}
