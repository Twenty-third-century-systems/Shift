using System.Collections.Generic;

namespace Dab.Dtos {
    public class ExternalUserDashboardDto {
        public int ApplicationsSubmitted { get; set; }
        public int RegisteredEntities { get; set; }
        public double AccountBalance { get; set; }
        public List<RecentActivityDto> RecentActivities { get; set; }
        public List<ApprovedApplicationDto> ApprovedApplications { get; set; }
    }
}