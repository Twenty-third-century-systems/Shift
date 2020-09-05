using Microsoft.AspNetCore.Identity;

namespace Shish.Models {
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser {
        // public string UserRole { get; set; }        
        
        public string Policy { get; set; }
    }
}