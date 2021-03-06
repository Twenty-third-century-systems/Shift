//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1591

using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

namespace Cooler.DataModels
{
	/// <summary>
	/// Database       : each
	/// Data Source    : DESKTOP-RMM46MK
	/// Server Version : 12.00.2000
	/// </summary>
	public partial class EachDB : LinqToDB.Data.DataConnection
	{
		public ITable<AmmendedArticle>           AmmendedArticles            { get { return this.GetTable<AmmendedArticle>(); } }
		public ITable<Application>               Applications                { get { return this.GetTable<Application>(); } }
		public ITable<ArticleOfAssociation>      ArticleOfAssociations       { get { return this.GetTable<ArticleOfAssociation>(); } }
		public ITable<ForeignEntity>             ForeignEntities             { get { return this.GetTable<ForeignEntity>(); } }
		public ITable<MemoObject>                MemoObjects                 { get { return this.GetTable<MemoObject>(); } }
		public ITable<Memorundum>                Memorundums                 { get { return this.GetTable<Memorundum>(); } }
		public ITable<Name>                      Names                       { get { return this.GetTable<Name>(); } }
		public ITable<NameSearch>                NameSearches                { get { return this.GetTable<NameSearch>(); } }
		public ITable<Office>                    Offices                     { get { return this.GetTable<Office>(); } }
		public ITable<PvtEntity>                 PvtEntities                 { get { return this.GetTable<PvtEntity>(); } }
		public ITable<PvtEntityHasForeignEntity> PvtEntityHasForeignEntities { get { return this.GetTable<PvtEntityHasForeignEntity>(); } }
		public ITable<PvtEntityHasPvtEntity>     PvtEntityHasPvtEntities     { get { return this.GetTable<PvtEntityHasPvtEntity>(); } }
		public ITable<PvtEntityHasSubcriber>     PvtEntityHasSubcribers      { get { return this.GetTable<PvtEntityHasSubcriber>(); } }
		public ITable<Role>                      Roles                       { get { return this.GetTable<Role>(); } }
		public ITable<Subcriber>                 Subcribers                  { get { return this.GetTable<Subcriber>(); } }
		public ITable<Subscription>              Subscriptions               { get { return this.GetTable<Subscription>(); } }
		public ITable<Task>                      Tasks                       { get { return this.GetTable<Task>(); } }

		// public EachDB()
		// {
		// 	InitDataContext();
		// 	InitMappingSchema();
		// }

		public EachDB(LinqToDbConnectionOptions<EachDB> options)
		: base(options)
		{
			// InitDataContext();
			// InitMappingSchema();
		}

		// partial void InitDataContext  ();
		// partial void InitMappingSchema();
	}

	[Table(Schema="each", Name="ammended_article")]
	public partial class AmmendedArticle
	{
		[Column("id"),         PrimaryKey, Identity] public int    Id        { get; set; } // int
		[Column("value"),      NotNull             ] public string Value     { get; set; } // nvarchar(max)
		[Column("article_id"), NotNull             ] public int    ArticleId { get; set; } // int
	}

	[Table(Schema="each", Name="application")]
	public partial class Application
	{
		[Column("id"),             PrimaryKey,  Identity] public int       Id            { get; set; } // int
		[Column("user_id"),        NotNull              ] public string    UserId        { get; set; } // nvarchar(50)
		[Column("service_id"),     NotNull              ] public int       ServiceId     { get; set; } // int
		[Column("date_submitted"), NotNull              ] public DateTime  DateSubmitted { get; set; } // datetime2(6)
		[Column("status"),         NotNull              ] public int       Status        { get; set; } // int
		[Column("sorting_office"), NotNull              ] public int       SortingOffice { get; set; } // int
		[Column("date_examined"),     Nullable          ] public DateTime? DateExamined  { get; set; } // datetime2(6)
		[Column("credit_id"),         Nullable          ] public int?      CreditId      { get; set; } // int
		[Column("task_id"),           Nullable          ] public int?      TaskId        { get; set; } // int
	}

	[Table(Schema="each", Name="article_of_association")]
	public partial class ArticleOfAssociation
	{
		[Column("id"),      PrimaryKey, Identity] public int    Id     { get; set; } // int
		[Column("table_A"), Nullable            ] public short? TableA { get; set; } // smallint
		[Column("table_B"), Nullable            ] public short? TableB { get; set; } // smallint
		[Column("other"),   Nullable            ] public short? Other  { get; set; } // smallint
	}

	[Table(Schema="each", Name="foreign_entity")]
	public partial class ForeignEntity
	{
		[Column("id"),          PrimaryKey, Identity] public int    Id         { get; set; } // int
		[Column("country"),     NotNull             ] public string Country    { get; set; } // nvarchar(45)
		[Column("company_ref"), NotNull             ] public string CompanyRef { get; set; } // nvarchar(45)
		[Column("name"),        NotNull             ] public string Name       { get; set; } // nvarchar(45)
	}

	[Table(Schema="each", Name="memo_object")]
	public partial class MemoObject
	{
		[Column("id"),            PrimaryKey, Identity] public int    Id           { get; set; } // int
		[Column("value"),         NotNull             ] public string Value        { get; set; } // nvarchar(max)
		[Column("memorundum_id"), NotNull             ] public int    MemorundumId { get; set; } // int
	}

	[Table(Schema="each", Name="memorundum")]
	public partial class Memorundum
	{
		[Column("id"),               PrimaryKey, Identity] public int    Id              { get; set; } // int
		[Column("share_clause"),     NotNull             ] public string ShareClause     { get; set; } // nvarchar(max)
		[Column("liability_clause"), NotNull             ] public string LiabilityClause { get; set; } // nvarchar(200)
	}

	[Table(Schema="each", Name="name")]
	public partial class Name
	{
		[Column("id"),             PrimaryKey, Identity] public int    Id           { get; set; } // int
		[Column("value"),          NotNull             ] public string Value        { get; set; } // nvarchar(200)
		[Column("status"),         NotNull             ] public int    Status       { get; set; } // int
		[Column("name_search_id"), NotNull             ] public string NameSearchId { get; set; } // nvarchar(50)
	}

	[Table(Schema="each", Name="name_search")]
	public partial class NameSearch
	{
		[Column("id"),                PrimaryKey,  NotNull] public string    Id              { get; set; } // nvarchar(50)
		[Column("service"),                        NotNull] public int       Service         { get; set; } // int
		[Column("justification"),                  NotNull] public string    Justification   { get; set; } // nvarchar(200)
		[Column("designation_id"),                 NotNull] public int       DesignationId   { get; set; } // int
		[Column("expiry_date"),          Nullable         ] public DateTime? ExpiryDate      { get; set; } // datetime2(6)
		[Column("application_id"),                 NotNull] public int       ApplicationId   { get; set; } // int
		[Column("reason_for_search"),              NotNull] public int       ReasonForSearch { get; set; } // int
		[Column("reference"),            Nullable         ] public string    Reference       { get; set; } // nvarchar(45)
	}

	[Table(Schema="each", Name="office")]
	public partial class Office
	{
		[Column("id"),               PrimaryKey, Identity] public int    Id              { get; set; } // int
		[Column("physical_address"), NotNull             ] public string PhysicalAddress { get; set; } // nvarchar(200)
		[Column("postal_address"),   NotNull             ] public string PostalAddress   { get; set; } // nvarchar(200)
		[Column("city"),             NotNull             ] public int    City            { get; set; } // int
		[Column("mobile_number"),    NotNull             ] public string MobileNumber    { get; set; } // nvarchar(45)
		[Column("telephone_number"), NotNull             ] public string TelephoneNumber { get; set; } // nvarchar(45)
		[Column("email_address"),    NotNull             ] public string EmailAddress    { get; set; } // nvarchar(200)
	}

	[Table(Schema="each", Name="pvt_entity")]
	public partial class PvtEntity
	{
		[Column("id"),                  PrimaryKey,  NotNull] public string Id                { get; set; } // nvarchar(50)
		[Column("application_id"),                   NotNull] public int    ApplicationId     { get; set; } // int
		[Column("last_application_id"),    Nullable         ] public int?   LastApplicationId { get; set; } // int
		[Column("name_id"),                Nullable         ] public int?   NameId            { get; set; } // int
		[Column("office_id"),              Nullable         ] public int?   OfficeId          { get; set; } // int
		[Column("articles_id"),            Nullable         ] public int?   ArticlesId        { get; set; } // int
		[Column("memorundum_id"),          Nullable         ] public int?   MemorundumId      { get; set; } // int
		[Column("reference"),              Nullable         ] public string Reference         { get; set; } // nvarchar(45)
	}

	[Table(Schema="each", Name="pvt_entity_has_foreign_entity")]
	public partial class PvtEntityHasForeignEntity
	{
		[Column("pvt_entity"),      PrimaryKey(1), NotNull] public string PvtEntity      { get; set; } // nvarchar(50)
		[Column("foreign_entity"),  PrimaryKey(2), NotNull] public int    ForeignEntity  { get; set; } // int
		[Column("subscription_id"),                NotNull] public int    SubscriptionId { get; set; } // int
	}

	[Table(Schema="each", Name="pvt_entity_has_pvt_entity")]
	public partial class PvtEntityHasPvtEntity
	{
		[Column("owner"),           PrimaryKey(1), NotNull] public string Owner          { get; set; } // nvarchar(50)
		[Column("owned"),           PrimaryKey(2), NotNull] public string Owned          { get; set; } // nvarchar(50)
		[Column("subscription_id"),                NotNull] public int    SubscriptionId { get; set; } // int
	}

	[Table(Schema="each", Name="pvt_entity_has_subcriber")]
	public partial class PvtEntityHasSubcriber
	{
		[Column("entity"),          PrimaryKey(1), NotNull] public string Entity         { get; set; } // nvarchar(50)
		[Column("subcriber"),       PrimaryKey(2), NotNull] public int    Subcriber      { get; set; } // int
		[Column("roles_id"),                       NotNull] public int    RolesId        { get; set; } // int
		[Column("subscription_id"),                NotNull] public int    SubscriptionId { get; set; } // int
	}

	[Table(Schema="each", Name="roles")]
	public partial class Role
	{
		[Column("id"),        PrimaryKey, Identity] public int    Id        { get; set; } // int
		[Column("director"),  Nullable            ] public short? Director  { get; set; } // smallint
		[Column("member"),    Nullable            ] public short? Member    { get; set; } // smallint
		[Column("secretary"), Nullable            ] public short? Secretary { get; set; } // smallint
	}

	[Table(Schema="each", Name="subcriber")]
	public partial class Subcriber
	{
		[Column("id"),               PrimaryKey,  Identity] public int    Id              { get; set; } // int
		[Column("country_code"),     NotNull              ] public string CountryCode     { get; set; } // nvarchar(200)
		[Column("national_id"),      NotNull              ] public string NationalId      { get; set; } // nvarchar(200)
		[Column("surname"),          NotNull              ] public string Surname         { get; set; } // nvarchar(200)
		[Column("first_name"),       NotNull              ] public string FirstName       { get; set; } // nvarchar(200)
		[Column("gender"),           NotNull              ] public int    Gender          { get; set; } // int
		[Column("physical_address"),    Nullable          ] public string PhysicalAddress { get; set; } // nvarchar(200)
	}

	[Table(Schema="each", Name="subscription")]
	public partial class Subscription
	{
		[Column("id"),              PrimaryKey, Identity] public int    Id              { get; set; } // int
		[Column("ordinary"),        Nullable            ] public long?  Ordinary        { get; set; } // bigint
		[Column("preference"),      Nullable            ] public long?  Preference      { get; set; } // bigint
		[Column("subscriptioncol"), Nullable            ] public string Subscriptioncol { get; set; } // nvarchar(45)
	}

	[Table(Schema="each", Name="task")]
	public partial class Task
	{
		[Column("id"),                          PrimaryKey, Identity] public int      Id                       { get; set; } // int
		[Column("examiner_id"),                 NotNull             ] public string   ExaminerId               { get; set; } // nvarchar(45)
		[Column("date_assigned"),               NotNull             ] public DateTime DateAssigned             { get; set; } // datetime2(6)
		[Column("assigned_by"),                 NotNull             ] public string   AssignedBy               { get; set; } // nvarchar(45)
		[Column("expected_date_of_completion"), NotNull             ] public DateTime ExpectedDateOfCompletion { get; set; } // datetime2(6)
	}

	public static partial class TableExtensions
	{
		public static AmmendedArticle Find(this ITable<AmmendedArticle> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Application Find(this ITable<Application> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ArticleOfAssociation Find(this ITable<ArticleOfAssociation> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ForeignEntity Find(this ITable<ForeignEntity> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static MemoObject Find(this ITable<MemoObject> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Memorundum Find(this ITable<Memorundum> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Name Find(this ITable<Name> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static NameSearch Find(this ITable<NameSearch> table, string Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Office Find(this ITable<Office> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static PvtEntity Find(this ITable<PvtEntity> table, string Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static PvtEntityHasForeignEntity Find(this ITable<PvtEntityHasForeignEntity> table, string PvtEntity, int ForeignEntity)
		{
			return table.FirstOrDefault(t =>
				t.PvtEntity     == PvtEntity &&
				t.ForeignEntity == ForeignEntity);
		}

		public static PvtEntityHasPvtEntity Find(this ITable<PvtEntityHasPvtEntity> table, string Owner, string Owned)
		{
			return table.FirstOrDefault(t =>
				t.Owner == Owner &&
				t.Owned == Owned);
		}

		public static PvtEntityHasSubcriber Find(this ITable<PvtEntityHasSubcriber> table, string Entity, int Subcriber)
		{
			return table.FirstOrDefault(t =>
				t.Entity    == Entity &&
				t.Subcriber == Subcriber);
		}

		public static Role Find(this ITable<Role> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Subcriber Find(this ITable<Subcriber> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Subscription Find(this ITable<Subscription> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Task Find(this ITable<Task> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}
	}
}

#pragma warning restore 1591
