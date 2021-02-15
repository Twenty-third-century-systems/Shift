using System;
using System.Collections.Generic;

#nullable disable

namespace Zett.World
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Countrylanguages = new HashSet<Countrylanguage>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Region { get; set; }
        public decimal SurfaceArea { get; set; }
        public short? IndepYear { get; set; }
        public int Population { get; set; }
        public decimal? LifeExpectancy { get; set; }
        public decimal? Gnp { get; set; }
        public decimal? Gnpold { get; set; }
        public string LocalName { get; set; }
        public string GovernmentForm { get; set; }
        public string HeadOfState { get; set; }
        public int? Capital { get; set; }
        public string Code2 { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Countrylanguage> Countrylanguages { get; set; }
    }
}
