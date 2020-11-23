using System;
using System.Collections.Generic;

namespace Till.Models {
    public class Payment {
        public Payment()
        {
            Credits = new HashSet<Credit>();
        }

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
        public string Email { get; set; }
        public string? PaynowRef { get; set; }
        public DateTime Date { get; set; }
        public string? ModeOfPayment { get; set; }
        public string? PollUrl { get; set; }
        public bool Success { get; set; }
        public string Description { get; set; }
        public double? CreditAmount { get; set; }
        public double? DebitAmount { get; set; }

        public virtual ICollection<Credit> Credits { get; set; }
    }
}