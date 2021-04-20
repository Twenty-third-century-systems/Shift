using Fridge.Models;
using Fridge.Models.Payments;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Contexts {
    public class PaymentsDatabaseContext : DbContext {
        public PaymentsDatabaseContext()
        {
            
        }
        public PaymentsDatabaseContext(DbContextOptions<PaymentsDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PriceItem> PriceItems { get; set; }
        public DbSet<Balance> Balances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=pytDB;Trusted_Connection=True;Enlist=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("payments");

                entity.Property(e => e.TransactionId).HasColumnName("Reference");

                entity.Property(e => e.User).HasColumnName("user");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.WalletProvider).HasColumnName("wallet_provider");

                entity.Property(e => e.PollUrl).HasColumnName("url");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone");

                entity.Property(e => e.PayNowReference).HasColumnName("paynow_ref");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.CreditAmount).HasColumnName("cr");

                entity.Property(e => e.DebitAmount).HasColumnName("dr");
            });

            modelBuilder.Entity<PriceItem>(entity =>
            {
                entity.ToTable("prices");

                entity.Property(e => e.PriceItemId).HasColumnName("id");

                entity.Property(e => e.Service).HasColumnName("for");

                entity.Property(e => e.Price).HasColumnName("amount");
            });

            modelBuilder.Entity<Balance>(entity =>
            {
                entity.ToTable("balances");

                entity.Property(e => e.BalanceId).HasColumnName("id");

                entity.Property(e => e.User).HasColumnName("user");

                entity.Property(e => e.Amount).HasColumnName("balance");
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}