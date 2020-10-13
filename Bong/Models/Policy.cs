using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bong.Models {
    public class Policy {
        [Key] public int Id { get; set; }
        public string Value { get; set; }
        public ICollection<ApplicationUserPolicy> Policies { get; set; }
    }
}