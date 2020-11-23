using System.Collections.Generic;
using Till.Models;

namespace Till.Dtos {
    public class AccountPaymentsDto {
        public List<Payment> Transactions { get; set; }
        public double Balance { get; set; }
    }
}