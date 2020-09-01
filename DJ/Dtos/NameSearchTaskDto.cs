using System.Collections.Generic;
using DJ.Models;

namespace DJ.Dtos {
    public class NameSearchTaskDto {
        public Application Application { get; set; }
        public NameSearch NameSearch { get; set; }
    }
}