using System.Collections.Generic;

namespace Cabinet.Dtos.Response {
    public class ExternalUserDashboardRequestDto {
        public ExternalUserDashboardRequestDto()
        {
            RecentActivity = new List<SubmittedApplicationResponseDto>();
            ApprovedApplications = new List<SubmittedApplicationResponseDto>();
        }
        public int SubmittedApplicationsCount { get; set; }
        public int RegisteredEntitiesCount { get; set; }
        public double AccountBalance { get; set; }
        public IEnumerable<SubmittedApplicationResponseDto> RecentActivity { get; set; }        
        public IEnumerable<SubmittedApplicationResponseDto> ApprovedApplications { get; set; }
    }
}