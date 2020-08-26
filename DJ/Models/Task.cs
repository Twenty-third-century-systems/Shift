using System;

namespace DJ.Models {
    public class Task {
        public int Id { get; set; }
        public int ApplicationCount { get; set; }
        public string Service { get; set; }
        public DateTime ExpectedDateOfCompletion { get; set; }
    }
}