using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Shish.Models {
    public class Personal {
        [Key] public string UserId { get; set; }
        [PersonalData] public string Names { get; set; }
        [PersonalData] public string Surname { get; set; }

        [PersonalData] public string NationalId { get; set; }

        public Address Address { get; set; }
        
    }
}