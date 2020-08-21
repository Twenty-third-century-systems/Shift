using System;
using System.Collections.Generic;
using Dab.Models;

namespace BarTender.Dtos{
    public class NameSearchRequestDto{

        public NameSearchDetails Details { get; set; }
        public List<string> Names { get; set; }
    }
}