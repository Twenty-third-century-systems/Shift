using System.Collections.Generic;
using DJ.Models;

namespace DJ.Dtos {
    public class AllApplicationsDto {
        public List<Application> NameSearchApplications { get; set; }
        public List<Application> PvtEntityApplications { get; set; }
    }
}