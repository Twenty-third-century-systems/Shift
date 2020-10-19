using System;

namespace DJ.Dtos {
    public class TaskFromPrincipalDto {
        public int? Id { get; set; }
        public string Service { get; set; }
        public string Examiner { get; set; }
        public string Allocator { get; set; }
        public int NumberOfApplications { get; set; }
        public DateTime DateOfCompletion { get; set; }
    }
}