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

        public DbSet<AmendedArticle> AmendedArticles { get; set; }
        public DbSet<ServiceApplication> Applications { get; set; }
        public DbSet<ArticleOfAssociation> ArticleOfAssociations { get; set; }
        public DbSet<ForeignEntity> ForeignEntities { get; set; }
        public DbSet<MemorandumObject> MemoObjects { get; set; }
        public DbSet<MemorandumOfAssociation> Memorandums { get; set; }
        public DbSet<EntityName> Names { get; set; }
        public DbSet<NameSearch> NameSearches { get; set; }
        public DbSet<PrivateEntityOffice> PrivateEntityOffices { get; set; }
        public DbSet<PrivateEntity> PrivateEntities { get; set; }
        public DbSet<PrivateEntityHasForeignEntity> PvtEntityHasForeignEntities { get; set; }
        public DbSet<PrivateEntityHasPrivateEntity> PvtEntityHasPvtEntities { get; set; }
        public DbSet<PrivateEntityHasSubscriber> PvtEntityHasSubscribers { get; set; }
        public DbSet<PrivateEntityRoles> Roles { get; set; }
        public DbSet<PrivateEntitySubscriber> Subscribers { get; set; }
        public DbSet<PrivateEntitySubscription> Subscriptions { get; set; }
        public DbSet<ExaminationTask> ExaminationTasks { get; set; }
        public DbSet<ProcessingDepartment> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ReasonForNameSearch> ReasonForSearches { get; set; }
        public DbSet<ServiceType> Services { get; set; }
        public DbSet<ApplicationStatus> Statuses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the EntityName= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(
                    "Server=localhost;Database=bigDB;Trusted_Connection=True;Enlist=False;");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AmendedArticle>(entity =>
            {
                entity.ToTable("amended_article");

                entity.HasIndex(e => e.ArticleId);

                entity.Property(e => e.AmendedArticleId).HasColumnName("id");

                entity.Property(e => e.ArticleId).HasColumnName("article");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value");

                entity.HasOne(d => d.ArticleOfAssociation)
                    .WithMany(p => p.AmendedArticles)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ServiceApplication>(entity =>
            {
                entity.ToTable("application");

                entity.HasIndex(e => e.TaskId);

                entity.HasIndex(e => e.ServiceId);

                entity.HasIndex(e => e.StatusId);

                entity.HasIndex(e => e.CityId);

                entity.Property(e => e.ServiceApplicationId).HasColumnName("id");

                entity.Property(e => e.DateExamined).HasColumnName("examined_on");

                entity.Property(e => e.DateSubmitted).HasColumnName("submitted_on");

                entity.Property(e => e.ServiceId).HasColumnName("service");

                entity.Property(e => e.CityId).HasColumnName("sorting_office");

                entity.Property(e => e.StatusId).HasColumnName("status");

                entity.Property(e => e.TaskId).HasColumnName("task");

                entity.Property(e => e.SoftDeleted)
                    .HasColumnName("deleted")
                    .HasDefaultValue(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user");

                entity.Property(e => e.SoftDeleted)
                    .HasColumnName("deleted");                

                entity.HasOne(d => d.ExaminationTask)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.TaskId);

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.ServiceId);

                entity.HasOne(d => d.ApplicationStatus)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.StatusId);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.CityId);
            });

            modelBuilder.Entity<ServiceApplication>().HasQueryFilter(e => !e.SoftDeleted);

            modelBuilder.Entity<ArticleOfAssociation>(entity =>
            {
                entity.ToTable("article_of_association");

                entity.Property(e => e.ArticleOfAssociationId).HasColumnName("id");

                entity.Property(e => e.Other).HasColumnName("other");

                entity.Property(e => e.TableA).HasColumnName("table_A");

                entity.Property(e => e.TableB).HasColumnName("table_B");
            });

            modelBuilder.Entity<ForeignEntity>(entity =>
            {
                entity.ToTable("foreign_entity");

                entity.Property(e => e.ForeignEntityId).HasColumnName("id");

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

            modelBuilder.Entity<MemorandumObject>(entity =>
            {
                entity.ToTable("memorandum_object");

                entity.HasIndex(e => e.MemorandumId);

                entity.Property(e => e.MemorandumObjectId).HasColumnName("id");

                entity.Property(e => e.MemorandumId).HasColumnName("memorandum");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value");

                entity.HasOne(d => d.MemorandumOfAssociation)
                    .WithMany(p => p.MemorandumObjects)
                    .HasForeignKey(d => d.MemorandumId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MemorandumOfAssociation>(entity =>
            {
                entity.ToTable("memorandum_of_association");

                entity.Property(e => e.MemorandumOfAssociationId).HasColumnName("id");

                entity.Property(e => e.LiabilityClause)
                    .IsRequired()
                    .HasColumnName("liability_clause");

                entity.Property(e => e.ShareClause)
                    .IsRequired()
                    .HasColumnName("share_clause");
            });

            modelBuilder.Entity<EntityName>(entity =>
            {
                entity.ToTable("suggested_name");

                entity.HasIndex(e => e.StatusId);

                entity.HasIndex(e => e.NameSearchId);

                entity.Property(e => e.EntityNameId).HasColumnName("id");

                entity.Property(e => e.NameSearchId).HasColumnName("name_search");

                entity.Property(e => e.StatusId).HasColumnName("status");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("value");

                entity.HasOne(d => d.NameSearch)
                    .WithMany(p => p.EntityNames)
                    .HasForeignKey(d => d.NameSearchId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ApplicationStatus)
                    .WithMany(p => p.EntityNames)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<NameSearch>(entity =>
            {
                entity.ToTable("name_search");

                entity.HasIndex(e => e.ServiceApplicationId);

                entity.HasIndex(e => e.Reference)
                    .IsUnique();

                entity.Property(e => e.NameSearchId).HasColumnName("id");

                entity.Property(e => e.ServiceApplicationId).HasColumnName("application");

                entity.Property(e => e.DesignationId).HasColumnName("designation");

                entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");

                entity.Property(e => e.Justification)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("justification");

                entity.Property(e => e.ReasonForSearchId).HasColumnName("reason_for_search");

                entity.Property(e => e.Reference).HasColumnName("ref");

                entity.Property(e => e.ServiceId).HasColumnName("service");                    
                    
                entity.HasOne(d => d.ServiceApplication)
                    .WithOne(p => p.NameSearch)
                    .HasForeignKey<NameSearch>(p => p.ServiceApplicationId);
                
                entity.HasOne(d => d.Service)
                    .WithMany(p => p.NameSearches)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.NameSearches)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ReasonForNameSearch)
                    .WithMany(p => p.NameSearches)
                    .HasForeignKey(d => d.ReasonForSearchId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PrivateEntityOffice>(entity =>
            {
                entity.ToTable("office");

                entity.HasIndex(e => e.CityId);
                
                entity.Property(e => e.PrivateEntityOfficeId).HasColumnName("id");

                entity.Property(e => e.CityId).HasColumnName("city");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("email_address");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("mobile_number");

                entity.Property(e => e.PhysicalAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("physical_address");

                entity.Property(e => e.PostalAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("postal_address");

                entity.Property(e => e.TelephoneNumber)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("telephone_number");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.EntityOffices)
                    .HasForeignKey(p => p.CityId);
            });

            modelBuilder.Entity<PrivateEntity>(entity =>
            {
                entity.ToTable("private_entity");

                entity.HasIndex(e => e.ApplicationId);

                entity.HasIndex(e => e.LastApplicationId);

                entity.HasIndex(e => e.ArticlesOfAssociationId);

                entity.HasIndex(e => e.MemorandumOfAssociationId);

                entity.HasIndex(e => e.EntityNameId);

                entity.HasIndex(e => e.EntityOfficeId);

                entity.Property(e => e.PrivateEntityId)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.ApplicationId).HasColumnName("application");

                entity.Property(e => e.ArticlesOfAssociationId).HasColumnName("articles");

                entity.Property(e => e.LastApplicationId).HasColumnName("last_application");

                entity.Property(e => e.MemorandumOfAssociationId).HasColumnName("memorandum");

                entity.Property(e => e.EntityNameId).HasColumnName("name");

                entity.Property(e => e.EntityOfficeId).HasColumnName("office");

                entity.Property(e => e.Reference)
                    .HasMaxLength(45)
                    .HasColumnName("reference");

                entity.HasOne(d => d.ServiceApplication)
                    .WithOne(p => p.PrivateEntity)
                    .HasForeignKey<PrivateEntity>(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.LastApplication)
                    .WithOne(p => p.PrivateEntityLastApplication)
                    .HasForeignKey<PrivateEntity>(d => d.LastApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ArticleOfAssociation)
                    .WithMany(p => p.PvtEntities)
                    .HasForeignKey(d => d.ArticlesOfAssociationId);

                entity.HasOne(d => d.MemorandumOfAssociation)
                    .WithMany(p => p.PrivateEntities)
                    .HasForeignKey(d => d.MemorandumOfAssociationId);

                entity.HasOne(d => d.EntityName)
                    .WithMany(p => p.PvtEntities)
                    .HasForeignKey(d => d.EntityNameId);

                entity.HasOne(d => d.PrivateEntityOffice)
                    .WithMany(p => p.PrivateEntities)
                    .HasForeignKey(d => d.EntityOfficeId);
            });
            
            
            // TODO: the following two relationships should be dissolved
            modelBuilder.Entity<PrivateEntityHasForeignEntity>(entity =>
            {
                entity.HasKey(e => new {PvtEntity = e.PrivateEntityId, ForeignEntity = e.ForeignEntityId});

                entity.ToTable("pvt_entity_has_foreign_entity");

                entity.HasIndex(e => e.ForeignEntityId);

                entity.HasIndex(e => e.PrivateEntityId);

                entity.HasIndex(e => e.SubscriptionId);

                entity.Property(e => e.PrivateEntityId)
                    .HasMaxLength(50)
                    .HasColumnName("pvt_entity");

                entity.Property(e => e.ForeignEntityId).HasColumnName("foreign_entity");

                entity.Property(e => e.SubscriptionId).HasColumnName("subscription");

                entity.HasOne(d => d.ForeignEntityNavigation)
                    .WithMany(p => p.PvtEntityHasForeignEntities)
                    .HasForeignKey(d => d.ForeignEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PrivateEntity)
                    .WithMany(p => p.PvtEntityHasForeignEntities)
                    .HasForeignKey(d => d.PrivateEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PrivateEntitySubscription)
                    .WithMany(p => p.PrivateEntityHasForeignEntities)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PrivateEntityHasPrivateEntity>(entity =>
            {
                entity.HasKey(e => new {Owner = e.OwnerId, Owned = e.OwnsId});

                entity.ToTable("pvt_entity_has_pvt_entity");

                entity.HasIndex(e => e.OwnerId);

                entity.HasIndex(e => e.OwnsId);

                entity.HasIndex(e => e.SubscriptionId);

                entity.Property(e => e.OwnerId)
                    .HasMaxLength(50)
                    .HasColumnName("owner");

                entity.Property(e => e.OwnsId)
                    .HasMaxLength(50)
                    .HasColumnName("owned");

                entity.Property(e => e.SubscriptionId).HasColumnName("subscription");

                entity.HasOne(d => d.OwnsNavigation)
                    .WithMany(p => p.PvtEntityHasPvtEntityOwnedNavigations)
                    .HasForeignKey(d => d.OwnsId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.PvtEntityHasPvtEntityOwnerNavigations)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PrivateEntitySubscription)
                    .WithMany(p => p.PrivateEntityHasPvtEntities)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PrivateEntityHasSubscriber>(entity =>
            {
                entity.HasKey(e => new {Entity = e.PrivateEntityId, Subcriber = e.SubscriberId});

                entity.ToTable("pvt_entity_has_subscriber");

                entity.HasIndex(e => e.PrivateEntityId);

                entity.HasIndex(e => e.RolesId);

                entity.HasIndex(e => e.SubscriberId);

                entity.HasIndex(e => e.SubscriptionId);

                entity.Property(e => e.PrivateEntityId)
                    .HasMaxLength(50)
                    .HasColumnName("entity");

                entity.Property(e => e.SubscriberId).HasColumnName("subscriber");

                entity.Property(e => e.RolesId).HasColumnName("role");

                entity.Property(e => e.SubscriptionId).HasColumnName("subscription");

                entity.HasOne(d => d.PrivateEntity)
                    .WithMany(p => p.PvtEntityHasSubcribers)
                    .HasForeignKey(d => d.PrivateEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RolesInPrivateEntityRoles)
                    .WithMany(p => p.PvtEntityHasSubscribers)
                    .HasForeignKey(d => d.RolesId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Subscriber)
                    .WithMany(p => p.PrivateEntityHasSubscribers)
                    .HasForeignKey(d => d.SubscriberId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PrivateEntitySubscription)
                    .WithMany(p => p.PrivateEntityHasSubscribers)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PrivateEntityRoles>(entity =>
            {
                entity.ToTable("private_entity_roles");

                entity.Property(e => e.PrivateEntityRolesId).HasColumnName("id");

                entity.Property(e => e.Director)
                    .HasColumnName("director")
                    .HasDefaultValue(false);

                entity.Property(e => e.Member)
                    .HasColumnName("member")
                    .HasDefaultValue(false);

                entity.Property(e => e.Secretary)
                    .HasColumnName("secretary")
                    .HasDefaultValue(false);
            });

            modelBuilder.Entity<PrivateEntitySubscriber>(entity =>
            {
                entity.ToTable("subscriber");

                entity.HasIndex(e => e.GenderId);

                entity.Property(e => e.PrivateEntitySubscriberId).HasColumnName("id");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("country_code");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("first_name");

                entity.Property(e => e.GenderId).HasColumnName("gender");

                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("national_id");

                entity.Property(e => e.PhysicalAddress)
                    .HasMaxLength(200)
                    .HasColumnName("physical_address");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("surname");

                entity.Property(e => e.IsARepresentative).HasColumnName("Represants");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.PrivateEntitySubscribers)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PrivateEntitySubscription>(entity =>
            {
                entity.ToTable("subscription");

                entity.Property(e => e.PrivateEntitySubscriptionId).HasColumnName("id");

                entity.Property(e => e.OrdinaryShares).HasColumnName("ordinary");

                entity.Property(e => e.PreferenceShares).HasColumnName("preference");
            });

            modelBuilder.Entity<ExaminationTask>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.ExaminationTaskId).HasColumnName("id");

                entity.Property(e => e.AssignedBy)
                    .IsRequired()
                    .HasColumnName("assigned_by");

                entity.Property(e => e.DateAssigned)
                    .HasPrecision(6)
                    .HasColumnName("date_assigned");

                entity.Property(e => e.ExaminerId)
                    .IsRequired()
                    .HasColumnName("assigned_to");

                entity.Property(e => e.ExpectedDateOfCompletion)
                    .HasPrecision(6)
                    .HasColumnName("expected_date_of_completion");
            });

            modelBuilder.Entity<ProcessingDepartment>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.ProcessingDepartmentId).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("title");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<Designation>(entity =>
            {
                entity.ToTable("designation");

                entity.Property(e => e.DesignationId).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");

                entity.Property(e => e.Description).HasColumnName("description");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("gender");

                entity.Property(e => e.GenderId).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<ReasonForNameSearch>(entity =>
            {
                entity.ToTable("reason_for_search");

                entity.Property(e => e.ReasonForNameSearchId).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.ToTable("service");

                entity.HasIndex(e => e.ProcessingDepartmentId);

                entity.Property(e => e.ServiceTypeId).HasColumnName("id");

                entity.Property(e => e.CanBeApplied).HasColumnName("can_be_applied");

                entity.Property(e => e.ProcessingDepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("description");

                // entity.Property(e => e.IsAnEntity).HasColumnName("is_an_entity");

                entity.HasOne(d => d.ProcessingDepartment)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ProcessingDepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ApplicationStatus>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.ApplicationStatusId).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("description");
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

            base.OnModelCreating(modelBuilder);
        }
    }
}