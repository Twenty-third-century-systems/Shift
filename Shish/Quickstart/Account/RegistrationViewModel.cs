using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI {
    public class RegistrationViewModel {
        [Required]
        public string Names { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        public string NationalId { get; set; }
        
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string RedirectUrl { get; set; }
    }
}