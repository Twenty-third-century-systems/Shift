using System.Collections.Generic;

namespace Dab.Models {
    public class NameApplicationAndDefaults {
        public int Id { get; set; }
        public string Value { get; set; }
        public int? ApplicationId { get; set; }
        public string PvtEntityId { get; set; } 
        public List<City> Cities { get; set; }
        public List<Country> Countries { get; set; }
        public List<Gender> Genders { get; set; }
    }
}