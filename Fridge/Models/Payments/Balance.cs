using System;
using System.ComponentModel.DataAnnotations;
using Fridge.Contexts;

namespace Fridge.Models.Payments {
    public class Balance {
        public int BalanceId { get; set; }
        public Guid User { get; set; }
        [ConcurrencyCheck]
        public double Amount { get; set; }

        public void UpDateAmount(PaymentsDatabaseContext context, double originalBalance, double newBalance)
        {
            Amount = newBalance;
            context.Entry(this).Property(b => b.Amount).OriginalValue = originalBalance;
        }
    }
}