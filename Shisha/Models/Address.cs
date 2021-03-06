﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Shish.Models {
    public class Address {
        public int Id { get; set; }        
        [PersonalData] public string ResidentialAddress { get; set; }
        [PersonalData] public string City { get; set; }
        [PersonalData] public string Country { get; set; }
    }
}