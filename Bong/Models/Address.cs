using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Bong.Models {
    public class Address {
        [Key] public int Id { get; set; }
        [PersonalData] public string ResidentialAddress { get; set; }
        [PersonalData] public string City { get; set; }
    }
}