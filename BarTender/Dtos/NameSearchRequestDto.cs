using System;
using System.Collections.Generic;
using BarTender.Models;

namespace BarTender.Dtos{
    public class NameSearchRequestDto{

        public NameSearchDetails Details { get; set; }
        public List<string> Names { get; set; }
    }
}