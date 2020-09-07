﻿using System.ComponentModel.DataAnnotations;

namespace Shish.Models {
    public class InternalUser {
        [Key] public string UserId { get; set; }
        public Person UserDetails { get; set; }
    }
}