using System;

namespace DanceFlow.Models {
    public class TaskSummary {
        public int Id { get; set; }
        public int ApplicationCount { get; set; }
        public string Service { get; set; }
        public DateTime ExpectedDateOfCompletion { get; set; }
    }
}