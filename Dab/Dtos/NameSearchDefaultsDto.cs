using System.Collections.Generic;
using Dab.Models;
using Val = Cabinet.Dtos.External.Response.Val;

namespace BarTender.Dtos {
    public class NameSearchDefaultsDto {
        public List<Val> Reasons { get; set; }
        public List<Val> Types { get; set; }
        public List<Val> Designations { get; set; }
        public List<Val> Sorters { get; set; }
    }
}