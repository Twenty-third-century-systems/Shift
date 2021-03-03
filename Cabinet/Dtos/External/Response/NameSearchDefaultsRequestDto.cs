using System.Collections.Generic;

namespace Cabinet.Dtos.External.Response {
    public class Val {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class NameSearchDefaultsRequestDto {
        public List<Val> ReasonForSearch { get; set; }
        public List<Val> Services { get; set; }
        public List<Val> Designations { get; set; }
        public List<Val> SortingOffices { get; set; }
    }
}