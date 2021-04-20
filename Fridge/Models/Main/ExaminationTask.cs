using System;
using System.Collections.Generic;
using Fridge.Constants;

#nullable disable

namespace Fridge.Models.Main
{
    public class ExaminationTask
    {
        public ExaminationTask()
        {
            Applications = new HashSet<Application>();
        }

        public int ExaminationTaskId { get; set; }
        public Guid Examiner { get; set; }
        public DateTime DateAssigned { get; set; }
        public Guid AssignedBy { get; set; }
        public DateTime ExpectedDateOfCompletion { get; set; }
        public ETaskStatus Status { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
