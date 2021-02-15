using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Zett.World
{
    public partial class shwaContext : DbContext
    {
        public shwaContext()
        {
        }

        public shwaContext(DbContextOptions<shwaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Countrylanguage> Countrylanguages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=shwa;Trusted_Connection=True;Enlist=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city", "world");

                entity.HasIndex(e => e.CountryCode, "CountryCode");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CanSort).HasDefaultValueSql("((0))");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.Population).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("city$city_ibfk_1");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_country_Code");

                entity.ToTable("country", "world");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.Code2)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.Continent)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'Asia')");

                entity.Property(e => e.Gnp)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("GNP");

                entity.Property(e => e.Gnpold)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("GNPOld");

                entity.Property(e => e.GovernmentForm)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.HeadOfState)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.LifeExpectancy).HasColumnType("numeric(3, 1)");

                entity.Property(e => e.LocalName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(52)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasMaxLength(26)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.SurfaceArea)
                    .HasColumnType("numeric(10, 2)")
                    .HasDefaultValueSql("((0.00))");
            });

            modelBuilder.Entity<Countrylanguage>(entity =>
            {
                entity.HasKey(e => new { e.CountryCode, e.Language })
                    .HasName("PK_countrylanguage_CountryCode");

                entity.ToTable("countrylanguage", "world");

                entity.HasIndex(e => e.CountryCode, "CountryCode");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.Language)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.IsOfficial)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'F')");

                entity.Property(e => e.Percentage).HasColumnType("numeric(4, 1)");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Countrylanguages)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countrylanguage$countryLanguage_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
