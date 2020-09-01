using System;

namespace DanceFlow.Models {
    public class Application {
        public int Id { get; set; }
        public string Service { get; set; }
        public string User { get; set; }
        public DateTime SubmissionDate { get; set; }        
        public bool Examined { get; set; }
    }
}