using BarTender.Models;

namespace BarTender.Dtos {
    public class ApprovedApplicationDto : SubmittedApplication {
        public string Reference { get; set; }
        public int NameSearchApplication { get; set; }
    }
}