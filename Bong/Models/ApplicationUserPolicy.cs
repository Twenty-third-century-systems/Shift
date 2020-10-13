using System.ComponentModel.DataAnnotations.Schema;

namespace Bong.Models {
    [Table("UserPolicy")]
    public class ApplicationUserPolicy {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int PolicyId { get; set; }
        public Policy Policy { get; set; }
    }
}