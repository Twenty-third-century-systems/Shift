using System;
using System.Collections.Generic;
using Dab.Models;

namespace Dab.Dtos {
    public class NameSearchResponseDto {
        public Guid Id { get; set; }
        public NameSearchDetails Details { get; set; }
        public List<string> Names { get; set; }
    }
}