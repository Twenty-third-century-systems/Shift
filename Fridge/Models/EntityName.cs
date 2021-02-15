using Fridge.Constants;

namespace Fridge.Models {
    public class EntityName {        
        public int EntityNameId { get; set; }
        public int NameSearchId { get; set; }
        public string Value { get; set; }
        public ENameStatus Status { get; set; }

        public NameSearch NameSearch { get; set; }
    }
}