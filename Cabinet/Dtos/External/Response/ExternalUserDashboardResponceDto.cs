using System.Collections.Generic;

namespace Cabinet.Dtos.External.Response {
    public class ExternalUserDashboardRequestDto {
        public ExternalUserDashboardRequestDto()
        {
            RecentActivity = new List<SubmittedApplicationSummaryResponseDto>();
            ApprovedApplications = new List<SubmittedApplicationSummaryResponseDto>();
        }
        public int SubmittedApplicationsCount { get; set; }
        public int RegisteredEntitiesCount { get; set; }
        public double AccountBalance { get; set; }
        public IEnumerable<SubmittedApplicationSummaryResponseDto> RecentActivity { get; set; }        
        public IEnumerable<SubmittedApplicationSummaryResponseDto> ApprovedApplications { get; set; }
    }
}