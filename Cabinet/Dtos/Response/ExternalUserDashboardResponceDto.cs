using System.Collections.Generic;

namespace Cabinet.Dtos.Response {
    public class ExternalUserDashboardRequestDto {        
        public int SubmittedApplicationsCount { get; set; }
        public int RegisteredEntitiesCount { get; set; }
        public double AccountBalance { get; set; }
        public IEnumerable<SubmittedApplicationRequestDto> RecentActivity { get; set; }        
        public IEnumerable<SubmittedApplicationRequestDto> ApprovedApplications { get; set; }
    }
}