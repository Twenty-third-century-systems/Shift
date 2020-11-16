using Microsoft.EntityFrameworkCore;
using Till.Models;

namespace Till.Contexts {
    public class DatabaseContext : DbContext {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<PriceList> PriceList { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");
                
                entity.Property(e => e.UserId).HasColumnName("user");
                
                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Email).HasColumnName("email");
                
                entity.Property(e => e.PaynowRef).HasColumnName("paynow_ref");
                
                entity.Property(e => e.Date).HasColumnName("date");
                
                entity.Property(e => e.Description).HasColumnName("description");
                
                entity.Property(e => e.CreditAmount).HasColumnName("cr");
                
                entity.Property(e => e.DebitAmount).HasColumnName("dr");
            });

            modelBuilder.Entity<Credit>(entity =>
            {
                entity.ToTable("credit", "dbo");

                entity.HasIndex(e => e.PaymentId).HasName("fk_credit_payment_idx1");

                entity.Property(e => e.Id).HasColumnName("id");
                
                entity.Property(e => e.PaymentId).HasColumnName("payment_id");
                
                entity.Property(e => e.Service).HasColumnName("service_id");
                
                entity.Property(e => e.ExpiryDate).HasColumnName("exp_date");
                
                entity.Property(e => e.Application).HasColumnName("application_id");

                entity.HasOne(e => e.PaymentNavigation)
                    .WithMany(f => f.Credits)
                    .HasForeignKey(e => e.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("credit$fk_credit_payment_idx");
            });

            modelBuilder.Entity<PriceList>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Service).HasColumnName("service");

                entity.Property(e => e.Price).HasColumnName("price");
            });
        }
    }
}