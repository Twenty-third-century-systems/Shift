using System;
using Fridge.Constants;

namespace Fridge.Models {
    public class Transaction {
        public Transaction()
        {
        }

        public Transaction(Guid user, EWalletProviders walletProvider, string email, string phoneNumber) : this(user)
        {
            WalletProvider = walletProvider;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Transaction(Guid user)
        {
            User = user;
            Date = DateTime.Now;
        }

        public Guid TransactionId { get; set; }
        public Guid User { get; set; }
        public DateTime Date { get; set; }
        public EWalletProviders WalletProvider { get; set; }
        public string PollUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PayNowReference { get; set; }
        public string Description { get; set; }
        public double? CreditAmount { get; set; }
        public double? DebitAmount { get; set; }

        public void TopUpDescription()
        {
            Description = "Top up";
        }

        public void NameSearchPaymentDescription(string reference)
        {
            Description = $"Name Search Payment: {reference}";
        }

        public void Credit(double amount)
        {
            CreditAmount = amount;
        }

        public void Debit(double amount)
        {
            DebitAmount = amount;
        }

        public decimal GetAmount()
        {
            return (decimal) CreditAmount.Value;
        }
    }
}