﻿using System;

namespace DJ.Models {
    public class Application {
        public int Id { get; set; }
        public string Service { get; set; }
        public string User { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}