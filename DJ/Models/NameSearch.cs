using System.Collections.Generic;

namespace DJ.Models {
    public class NameSearch {
        public string Id { get; set; }
        
        public string ReasonForSearch { get; set; }
        public string TypeOfEntity { get; set; }
        public string Designation { get; set; }
        public string Justification { get; set; }

        public List<Name> Names { get; set; } = new List<Name>();
    }
}