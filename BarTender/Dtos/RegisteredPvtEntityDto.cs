﻿using System;

namespace BarTender.Dtos {
    public class RegisteredPvtEntityDto {
        public string EntityName { get; set; }
        public string Reference { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}