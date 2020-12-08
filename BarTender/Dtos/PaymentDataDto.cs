using System;

namespace BarTender.Dtos {
    public class PaymentDataDto {
        public string Email { get; set; }
        public int Service { get; set; }
        public Guid UserId { get; set; }
    }
}