using System;

namespace Cabinet.Dtos.Internal.Request {
    public class NewTaskAllocationRequestDto {
        public int SortingOffice { get; set; }
        public int ApplicationId { get; set; }
        public int Service { get; set; }
        public Guid Examiner { get; set; }
        public DateTime DateAssigned { get; set; }
        public Guid AssignedBy { get; set; }
        public DateTime ExpectedDateOfCompletion { get; set; }
        public int NumberOfApplications { get; set; }
    }
}