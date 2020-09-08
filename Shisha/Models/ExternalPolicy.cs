﻿using System.Collections.Generic;

namespace Shish.Models {
    public class ExternalPolicy {
        public int Id { get; set; }
        public string Value { get; set; }
        public ExternalUser ExternalUser { get; set; }
    }
}