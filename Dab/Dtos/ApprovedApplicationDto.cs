using BarTender.Models;
using Dab.Models;

namespace Dab.Dtos {
    public class ApprovedApplicationDto : SubmittedApplication {
        public string Reference { get; set; }
        public int NameSearchApplication { get; set; }
    }
}