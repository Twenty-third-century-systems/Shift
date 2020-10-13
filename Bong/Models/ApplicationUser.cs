using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Bong.Models {
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser {
        [PersonalData] public string Firstname { get; set; }
        [AllowNull] [PersonalData] public string MiddleName { get; set; }
        [PersonalData] public string Surname { get; set; }
        [PersonalData] public string NationalId { get; set; }        
        [PersonalData] public int Office { get; set; }
        [PersonalData] public Address Address { get; set; }
        public ICollection<ApplicationUserPolicy> Policies { get; set; } = new List<ApplicationUserPolicy>();
    }
}