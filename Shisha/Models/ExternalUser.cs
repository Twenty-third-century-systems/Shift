﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Shish.Models {
    public class ExternalUser {
        [Key] public string UserId { get; set; }
        public Person UserDetails { get; set; }

        public List<ExternalPolicy> Policies { get; set; }
    }
}