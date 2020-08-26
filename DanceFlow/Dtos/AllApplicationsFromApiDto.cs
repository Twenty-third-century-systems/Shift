using System.Collections.Generic;
using DanceFlow.Models;

namespace DanceFlow.Dtos {
    public class AllApplicationsFromApiDto {
        public List<Application> NameSearchApplications { get; set; }
        public List<Application> PvtEntityApplications { get; set; }
    }
}