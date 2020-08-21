﻿using System;
using System.Collections.Generic;
using BarTender.Models;

namespace BarTender.Dtos {
    public class NameSearchResponseDto {
        public Guid Id { get; set; }
        public NameSearchDetails Details { get; set; }
        public List<string> Names { get; set; }
    }
}