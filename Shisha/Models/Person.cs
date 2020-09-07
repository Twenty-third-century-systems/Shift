﻿using Microsoft.AspNetCore.Identity;

namespace Shish.Models {
    public class Person {
        public int Id { get; set; }
        [PersonalData] public string Names { get; set; }
        [PersonalData] public string Surname { get; set; }
        [PersonalData] public string NationalId { get; set; }
        public Address Address { get; set; }
    }
}