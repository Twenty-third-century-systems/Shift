using System.Collections.Generic;

namespace Dab.Models {
    public class NameAndApplication {
        public int Id { get; set; }
        public string Value { get; set; }
        public int? ApplicationId { get; set; }
        public string PvtEntityId { get; set; } 
        public List<City> Cities { get; set; }
    }
}