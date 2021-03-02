using System;

namespace Cabinet.Dtos.Internal.Request {
    public class NewTaskAllocationRequestDto {
        public int SortingOffice { get; set; }
        // for single application allocation
        public int ApplicationId { get; set; }
        public int Service { get; set; }
        public Guid Examiner { get; set; }
        public DateTime DateAssigned { get; set; }
        public Guid AssignedBy { get; set; }
        public DateTime ExpectedDateOfCompletion { get; set; }
        // for multiple application allocation
        public int NumberOfApplications { get; set; }
    }
}