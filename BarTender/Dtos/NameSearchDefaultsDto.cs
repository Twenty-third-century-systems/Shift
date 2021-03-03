using System.Collections.Generic;
using BarTender.Models;
using Val = Cabinet.Dtos.External.Response.Val;

namespace BarTender.Dtos {
    public class NameSearchDefaultsDto {
        public IEnumerable<Val> Reasons { get; set; }
        public IEnumerable<Val> Types { get; set; }
        public IEnumerable<Val> Designations { get; set; }
        public IEnumerable<Val> Sorters { get; set; }
    }
}