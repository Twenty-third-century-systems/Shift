using System.ComponentModel.DataAnnotations;
using Bong.Models;

namespace Bong.Dtos {
    public class RegistrationDto {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string NationalId { get; set; }
        [Required]
        public int Office { get; set; }
        [Required]
        public string ResidentialAddress { get; set; }        
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}