using System;
using Fridge.Constants;
using Fridge.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Fridge.Contexts {
    public class MainDatabaseContext : DbContext {
        public MainDatabaseContext()
        {
        }

        public MainDatabaseContext(DbContextOptions<MainDatabaseContext> options)
            : base(options)
        {
        }

        // Uncomment this to app that moves country data

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=localhost;Database=bigDB;Trusted_Connection=True;Enlist=False;");
        // }

        public DbSet<Application> Applications { get; set; }
        public DbSet<ShareholdingForeignEntity> ForeignEntities { get; set; }
        public DbSet<NameSearch> NameSearches { get; set; }
        public DbSet<EntityName> Names { get; set; }
        public DbSet<PrivateEntity> PrivateEntities { get; set; }
        public DbSet<ExaminationTask> ExaminationTasks { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PrivateEntityHasPrivateEntity> ShareHoldingEntities { get; set; }
        public DbSet<PrivateEntityHasPrivateEntityOwner> EntityOwners { get; set; }
        public DbSet<Person> Subscribers { get; set; }
        public DbSet<PrivateEntityOwnerHasPrivateEntityOwner> Nominees { get; set; }
        public DbSet<PrivateEntityOwnerHasShareClause> Subscriptions { get; set; }
        public DbSet<ShareClause> ShareClasses { get; set; }
        public DbSet<ShareHoldingForeignEntityHasShareClause> ShareHoldingForeignEntities { get; set; }
        public DbSet<ShareHoldingForeignEntityHasPrivateEntityOwner> ForeignEntityShareholders { get; set; }
        public DbSet<MemorandumOfAssociation> MemorandumOfAssociations { get; set; }
        public DbSet<MemorandumOfAssociationObject> MemorandumOfAssociationObjects { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PriceItem> PriceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("application");

                entity.HasIndex(e => e.TaskId);

                entity.HasIndex(e => e.CityId);

                entity.Property(e => e.ApplicationId).HasColumnName("id");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasColumnName("user");

                entity.Property(e => e.Service)
                    .HasColumnName("service")
                    .HasConversion(c => c.ToString(), c => Enum.Parse<EService>(c));

                entity.Property(e => e.DateSubmitted).HasColumnName("submitted_on");

                entity.Property(e => e.DateExamined).HasColumnName("examined_on");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(c => c.ToString(), c => Enum.Parse<EApplicationStatus>(c));

                entity.Property(e => e.CityId).HasColumnName("sorting_office");

                entity.Property(e => e.TaskId).HasColumnName("task");

                entity.HasQueryFilter(e => !e.SoftDeleted);

                entity.Property(e => e.SoftDeleted)
                    .HasColumnName("deleted");

                entity.OwnsMany(e => e.RaisedQueries, r =>
                {
                    r.ToTable("query");

                    r.Property(e => e.Step).HasColumnName("application_step");

                    r.Property(e => e.Comment).HasColumnName("comment");
                });


                entity.HasOne(d => d.ExaminationTask)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.TaskId);

                entity.HasOne(d => d.SortingOffice)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.CityId);
            });

            modelBuilder.Entity<ShareholdingForeignEntity>(entity =>
            {
                entity.ToTable("foreign_entity");

                entity.Property(e => e.ShareholdingForeignEntityId).HasColumnName("id");

                entity.Property(e => e.CompanyReference)
                    .IsRequired()
                    .HasColumnName("company_reference");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasColumnName("country");

                entity.Property(e => e.ForeignEntityName)
                    .IsRequired()
                    .HasColumnName("name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ForeignEntities)
                    .HasForeignKey(d => d.CountryCode);
            });

            modelBuilder.Entity<NameSearch>(entity =>
            {
                entity.ToTable("name_search");

                entity.HasIndex(e => e.Reference)
                    .IsUnique();

                entity.Property(e => e.NameSearchId).HasColumnName("id");

                entity.Property(e => e.ApplicationId).HasColumnName("application");

                entity.Property(e => e.Service)
                    .HasColumnName("service")
                    .HasConversion(c => c.ToString(), c => Enum.Parse<EService>(c));

                entity.Property(e => e.Justification)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("justification");

                entity.Property(e => e.Designation)
                    .HasColumnName("designation")
                    .HasConversion(c => c.ToString(), c => Enum.Parse<EDesignation>(c));

                entity.Property(e => e.ReasonForSearch)
                    .HasColumnName("reason_for_search")
                    .HasConversion(c => c.ToString(), c => Enum.Parse<EReasonForSearch>(c));

                entity.Property(e => e.Reference).HasColumnName("ref");

                entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");

                entity.HasOne(d => d.Application)
                    .WithOne(p => p.NameSearch)
                    .HasForeignKey<NameSearch>(p => p.ApplicationId);
            });

            modelBuilder.Entity<EntityName>(entity =>
            {
                entity.ToTable("name");

                entity.HasIndex(e => e.NameSearchId);

                entity.Property(e => e.EntityNameId).HasColumnName("id");

                entity.Property(e => e.NameSearchId).HasColumnName("name_search");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion(c => c.ToString(), c => Enum.Parse<ENameStatus>(c));

                entity.HasOne(d => d.NameSearch)
                    .WithMany(p => p.Names)
                    .HasForeignKey(d => d.NameSearchId);
            });

            modelBuilder.Entity<PrivateEntity>(entity =>
            {
                entity.ToTable("private_entity");

                entity.HasIndex(e => e.ApplicationId);

                entity.HasIndex(e => e.LastApplicationId);

                entity.Property(e => e.PrivateEntityId)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.ApplicationId).HasColumnName("application");

                entity.Property(e => e.LastApplicationId).HasColumnName("last_application");

                entity.Property(e => e.Reference)
                    .HasMaxLength(45)
                    .HasColumnName("reference");

                entity.OwnsOne(e => e.Office, o =>
                {
                    o.ToTable("office");

                    o.Property(e => e.MobileNumber).HasColumnName("mobile");

                    o.Property(e => e.TelephoneNumber).HasColumnName("telephone");

                    o.Property(e => e.EmailAddress).HasColumnName("email");

                    o.OwnsOne(o => o.Address, a =>
                    {
                        a.Property(e => e.CityTown).HasColumnName("city");

                        a.Property(e => e.PhysicalAddress).HasColumnName("physical_address");

                        a.Property(e => e.PostalAddress).HasColumnName("postal_address");
                    }).ToTable("address");
                });

                entity.OwnsOne(e => e.ArticlesOfAssociation, a =>
                {
                    a.ToTable("article_of_association");
                    
                    a.Property(e => e.TableOfArticles)
                        .HasColumnName("table")
                        .HasConversion(c => c.ToString(), c => Enum.Parse<EArticlesOfAssociation>(c));

                    a.OwnsMany(e => e.AmendedArticles, am =>
                    {
                        am.ToTable("amended_article");
                        
                        am.Property(e => e.AmendedArticleId).HasColumnName("id");

                        am.Property(e => e.Value).HasColumnName("value");
                    });
                });

                entity.HasOne(d => d.CurrentApplication)
                    .WithOne(p => p.PrivateEntity)
                    .HasForeignKey<PrivateEntity>(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.LastApplication)
                    .WithOne(p => p.PrivateEntityLastApplication)
                    .HasForeignKey<PrivateEntity>(d => d.LastApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.NameSearchApplicationApplication)
                    .WithOne(p => p.PrivateEntityNameSearchApplication)
                    .HasForeignKey<PrivateEntity>(d => d.NameSearchApplicationId);
            });

            modelBuilder.Entity<ExaminationTask>(entity =>
            {
                entity.ToTable("application_task");

                entity.Property(e => e.ExaminationTaskId).HasColumnName("id");

                entity.Property(e => e.AssignedBy)
                    .IsRequired()
                    .HasColumnName("assigned_by");

                entity.Property(e => e.DateAssigned)
                    .HasPrecision(6)
                    .HasColumnName("date_assigned");

                entity.Property(e => e.Examiner)
                    .IsRequired()
                    .HasColumnName("assigned_to");

                entity.Property(e => e.ExpectedDateOfCompletion)
                    .HasPrecision(6)
                    .HasColumnName("expected_date_of_completion");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.HasIndex(e => e.CountryCode, "CountryCode");

                entity.Property(e => e.CityId).HasColumnName("ID");

                entity.Property(e => e.CanSort).HasDefaultValue(false);

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

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("country");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'')")
                    .IsFixedLength(true);

                entity.Property(e => e.Continent)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(N'Asia')");

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
            });

            modelBuilder.Entity<MemorandumOfAssociation>(entity =>
            {
                entity.ToTable("memo");

                entity.HasIndex(e => e.PrivateEntityId);

                entity.Property(e => e.MemorandumOfAssociationId).HasColumnName("id");

                entity.Property(e => e.PrivateEntityId).HasColumnName("entity");

                entity.Property(e => e.LiabilityClause).HasColumnName("liability_clause");

                entity.HasOne(d => d.PrivateEntity)
                    .WithOne(p => p.MemorandumOfAssociation)
                    .HasForeignKey<MemorandumOfAssociation>(d => d.PrivateEntityId);
            });

            modelBuilder.Entity<MemorandumOfAssociationObject>(entity =>
            {
                entity.ToTable("memo_objects");

                entity.HasIndex(e => e.MemorandumId);

                entity.Property(e => e.MemorandumOfAssociationObjectId).HasColumnName("id");

                entity.Property(e => e.MemorandumId).HasColumnName("memo");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Memorandum)
                    .WithMany(p => p.MemorandumObjects)
                    .HasForeignKey(d => d.MemorandumId);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("subscriber_nominee");

                entity.HasIndex(e => e.CountryCode);

                entity.Property(e => e.PrivateEntityOwnerId).HasColumnName("id");

                entity.Property(e => e.CountryCode).HasColumnName("country");

                entity.Property(e => e.Surname).HasColumnName("surname");

                entity.Property(e => e.Names).HasColumnName("names");

                entity.Property(e => e.Gender)
                    .HasColumnName("sex")
                    .HasConversion(c => c.ToString(), c => Enum.Parse<EGender>(c));

                entity.Property(e => e.DateOfBirth).HasColumnName("dob");

                entity.Property(e => e.NationalIdentification).HasColumnName("national_id_passport");

                entity.Property(e => e.PhysicalAddress).HasColumnName("physical_address");

                entity.Property(e => e.MobileNumber).HasColumnName("mobile_number");

                entity.Property(e => e.EmailAddress).HasColumnName("email_address");

                entity.Property(e => e.DateOfAppointment).HasColumnName("date_of_appointment");

                entity.Property(e => e.IsSecretary).HasColumnName("secretary");

                entity.Property(e => e.IsDirector).HasColumnName("director");

                entity.Property(e => e.Occupation).HasColumnName("occupation");

                entity.Property(e => e.DateOfTakeUp).HasColumnName("take_up_date");

                entity.Property(e => e.PrivateEntityOwnerId).HasColumnName("id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.PrivateOwners)
                    .HasForeignKey(d => d.CountryCode);
            });

            modelBuilder.Entity<ShareClause>(entity =>
            {
                entity.ToTable("share_clause");

                entity.HasIndex(e => e.MemorandumId);

                entity.Property(e => e.ShareClauseId).HasColumnName("id");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.NominalValue).HasColumnName("value");

                entity.Property(e => e.MemorandumId).HasColumnName("memo");

                entity.HasOne(d => d.MemorandumOfAssociation)
                    .WithMany(p => p.ShareClauses)
                    .HasForeignKey(d => d.MemorandumId);
            });

            modelBuilder.Entity<PrivateEntityHasPrivateEntity>(entity =>
            {
                entity.ToTable("private_entity_has_private_entity");

                entity.HasKey(e => new {Entity = e.EntityId, ShareHoldingEntity = e.ShareHoldingEntityId});

                entity.Property(e => e.EntityId).HasColumnName("owned");

                entity.Property(e => e.ShareHoldingEntityId).HasColumnName("owns");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.OwnedEntities)
                    .HasForeignKey(d => d.EntityId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.ShareHoldingEntity)
                    .WithMany(p => p.MemberEntities)
                    .HasForeignKey(d => d.ShareHoldingEntityId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<PrivateEntityHasPrivateEntityOwner>(entity =>
            {
                entity.ToTable("private_entity_has_private_entity_owner");

                entity.HasKey(e => new {e.PrivateEntityId, e.PrivateEntityOwnerId});

                entity.HasIndex(e => e.PrivateEntityId);

                entity.HasIndex(e => e.PrivateEntityOwnerId);

                entity.Property(e => e.PrivateEntityId).HasColumnName("entity");

                entity.Property(e => e.PrivateEntityOwnerId).HasColumnName("subscriber");

                entity.HasOne(d => d.Entity)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.PrivateEntityId);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ShareHoldingEntities)
                    .HasForeignKey(d => d.PrivateEntityOwnerId);
            });

            modelBuilder.Entity<PrivateEntityOwnerHasPrivateEntityOwner>(entity =>
            {
                entity.ToTable("private_entity_owner_has_private_entity_owner");

                entity.HasKey(e => new {e.BeneficiaryId, e.NomineeId});

                entity.HasIndex(e => e.BeneficiaryId);

                entity.HasIndex(e => e.NomineeId);

                entity.Property(e => e.BeneficiaryId).HasColumnName("beneficiary");

                entity.Property(e => e.NomineeId).HasColumnName("nominee");

                entity.HasOne(d => d.Beneficiary)
                    .WithMany(p => p.Nominees)
                    .HasForeignKey(d => d.BeneficiaryId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(d => d.Nominee)
                    .WithMany(p => p.Beneficiaries)
                    .HasForeignKey(d => d.NomineeId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<PrivateEntityOwnerHasShareClause>(entity =>
            {
                entity.ToTable("private_entity_owner_has_share_clause");

                entity.HasKey(e => new {e.PrivateEntityOwnerId, e.ShareClauseId});

                entity.HasIndex(e => e.PrivateEntityOwnerId);

                entity.HasIndex(e => e.ShareClauseId);

                entity.Property(e => e.PrivateEntityOwnerId).HasColumnName("subscriber");

                entity.Property(e => e.ShareClauseId).HasColumnName("subcriptions");

                entity.HasOne(d => d.Subscriber)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.PrivateEntityOwnerId);

                entity.HasOne(d => d.ShareClauseClass)
                    .WithMany(p => p.Subscribers)
                    .HasForeignKey(d => d.ShareClauseId);
            });

            modelBuilder.Entity<ShareHoldingForeignEntityHasShareClause>(entity =>
            {
                entity.ToTable("share_holding_foreign_enity_has_share_clause");

                entity.HasKey(e => new {PrivateEntityOwnerId = e.ShareHoldingForeignEntityId, e.ShareClauseId});

                entity.HasIndex(e => e.ShareHoldingForeignEntityId);

                entity.HasIndex(e => e.ShareClauseId);

                entity.Property(e => e.ShareHoldingForeignEntityId).HasColumnName("subscriber");

                entity.Property(e => e.ShareClauseId).HasColumnName("subcriptions");

                entity.HasOne(d => d.Subscriber)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.ShareHoldingForeignEntityId);

                entity.HasOne(d => d.ShareClauseClass)
                    .WithMany(p => p.ForeignEntityShareHolders)
                    .HasForeignKey(d => d.ShareClauseId);
            });

            modelBuilder.Entity<ShareHoldingForeignEntityHasPrivateEntityOwner>(entity =>
            {
                entity.ToTable("share_holding_foreign_entity_has_private_entity_owner");

                entity.HasKey(e => new {e.ForeignEntityId, e.PrivateEntityOwnerId});

                entity.HasIndex(e => e.ForeignEntityId);

                entity.HasIndex(e => e.PrivateEntityOwnerId);

                entity.Property(e => e.ForeignEntityId).HasColumnName("foreign_entity");

                entity.Property(e => e.PrivateEntityOwnerId).HasColumnName("represantative");

                entity.HasOne(d => d.ForeignEntity)
                    .WithMany(p => p.Representatives)
                    .HasForeignKey(d => d.ForeignEntityId);

                entity.HasOne(d => d.Nominee)
                    .WithMany(p => p.RepresentedForeignEntities)
                    .HasForeignKey(d => d.PrivateEntityOwnerId);
            });

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

            base.OnModelCreating(modelBuilder);
        }
    }
}