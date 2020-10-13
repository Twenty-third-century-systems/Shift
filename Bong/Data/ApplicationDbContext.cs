using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bong.Models;

namespace Bong.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses;

        public DbSet<Policy> Policies { get; set; }

        public DbSet<ApplicationUserPolicy> Type { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {    
            builder.Entity<ApplicationUserPolicy>()
                .HasKey(ap => new {ap.UserId,ap.PolicyId});  
            
            builder.Entity<ApplicationUserPolicy>()
                .HasOne(ap => ap.User)
                .WithMany(a => a.Policies)
                .HasForeignKey(ap => ap.UserId);  
            
            builder.Entity<ApplicationUserPolicy>()
                .HasOne(ap =>ap.Policy)
                .WithMany(p => p.Policies)
                .HasForeignKey(ap => ap.PolicyId);
            
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}