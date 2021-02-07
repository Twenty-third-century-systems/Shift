using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fridge.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article_of_association",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    table_A = table.Column<bool>(type: "bit", nullable: false),
                    table_B = table.Column<bool>(type: "bit", nullable: false),
                    other = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_of_association", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    Code = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false, defaultValueSql: "(N'')"),
                    Name = table.Column<string>(type: "char(52)", unicode: false, fixedLength: true, maxLength: 52, nullable: false, defaultValueSql: "(N'')"),
                    Continent = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: false, defaultValueSql: "(N'Asia')"),
                    Region = table.Column<string>(type: "char(26)", unicode: false, fixedLength: true, maxLength: 26, nullable: false, defaultValueSql: "(N'')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "designation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_designation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gender",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gender", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "memorandum_of_association",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    share_clause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    liability_clause = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memorandum_of_association", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "private_entity_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    director = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    member = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    secretary = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_private_entity_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reason_for_search",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reason_for_search", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subscription",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ordinary = table.Column<long>(type: "bigint", nullable: true),
                    preference = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assigned_to = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date_assigned = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: false),
                    assigned_by = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    expected_date_of_completion = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "amended_article",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    article = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amended_article", x => x.id);
                    table.ForeignKey(
                        name: "FK_amended_article_article_of_association_article",
                        column: x => x.article,
                        principalTable: "article_of_association",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "char(35)", unicode: false, fixedLength: true, maxLength: 35, nullable: false, defaultValueSql: "(N'')"),
                    CountryCode = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false, defaultValueSql: "(N'')"),
                    District = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: false, defaultValueSql: "(N'')"),
                    CanSort = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.ID);
                    table.ForeignKey(
                        name: "FK_city_country_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "country",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "foreign_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country = table.Column<string>(type: "char(3)", nullable: false),
                    company_reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foreign_entity", x => x.id);
                    table.ForeignKey(
                        name: "FK_foreign_entity_country_country",
                        column: x => x.country,
                        principalTable: "country",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "service",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    can_be_applied = table.Column<bool>(type: "bit", nullable: false),
                    department_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service", x => x.id);
                    table.ForeignKey(
                        name: "FK_service_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "subscriber",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country_code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    national_id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    physical_address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriber", x => x.id);
                    table.ForeignKey(
                        name: "FK_subscriber_gender_gender",
                        column: x => x.gender,
                        principalTable: "gender",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "memorandum_object",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    memorandum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memorandum_object", x => x.id);
                    table.ForeignKey(
                        name: "FK_memorandum_object_memorandum_of_association_memorandum",
                        column: x => x.memorandum,
                        principalTable: "memorandum_of_association",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "office",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    physical_address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    postal_address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    city = table.Column<int>(type: "int", nullable: false),
                    mobile_number = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    telephone_number = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    email_address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_office", x => x.id);
                    table.ForeignKey(
                        name: "FK_office_city_city",
                        column: x => x.city,
                        principalTable: "city",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    service = table.Column<int>(type: "int", nullable: true),
                    submitted_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true),
                    city = table.Column<int>(type: "int", nullable: true),
                    examined_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    task = table.Column<int>(type: "int", nullable: true),
                    deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application", x => x.id);
                    table.ForeignKey(
                        name: "FK_application_city_city",
                        column: x => x.city,
                        principalTable: "city",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_application_service_service",
                        column: x => x.service,
                        principalTable: "service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_application_status_status",
                        column: x => x.status,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_application_task_task",
                        column: x => x.task,
                        principalTable: "task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "name_search",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    service = table.Column<int>(type: "int", nullable: false),
                    justification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    designation = table.Column<int>(type: "int", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    application = table.Column<int>(type: "int", nullable: false),
                    reason_for_search = table.Column<int>(type: "int", nullable: false),
                    @ref = table.Column<string>(name: "ref", type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_name_search", x => x.id);
                    table.ForeignKey(
                        name: "FK_name_search_application_application",
                        column: x => x.application,
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_name_search_designation_designation",
                        column: x => x.designation,
                        principalTable: "designation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_name_search_reason_for_search_reason_for_search",
                        column: x => x.reason_for_search,
                        principalTable: "reason_for_search",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_name_search_service_service",
                        column: x => x.service,
                        principalTable: "service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "suggested_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    name_search = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suggested_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_suggested_name_name_search_name_search",
                        column: x => x.name_search,
                        principalTable: "name_search",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_suggested_name_status_status",
                        column: x => x.status,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "private_entity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    application = table.Column<int>(type: "int", nullable: false),
                    last_application = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<int>(type: "int", nullable: true),
                    office = table.Column<int>(type: "int", nullable: true),
                    articles = table.Column<int>(type: "int", nullable: true),
                    memorandum = table.Column<int>(type: "int", nullable: true),
                    reference = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    LastApplicationServiceApplicationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_private_entity", x => x.id);
                    table.ForeignKey(
                        name: "FK_private_entity_application_last_application",
                        column: x => x.last_application,
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_private_entity_application_LastApplicationServiceApplicationId",
                        column: x => x.LastApplicationServiceApplicationId,
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_private_entity_article_of_association_articles",
                        column: x => x.articles,
                        principalTable: "article_of_association",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_private_entity_memorandum_of_association_memorandum",
                        column: x => x.memorandum,
                        principalTable: "memorandum_of_association",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_private_entity_office_office",
                        column: x => x.office,
                        principalTable: "office",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_private_entity_suggested_name_name",
                        column: x => x.name,
                        principalTable: "suggested_name",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pvt_entity_has_foreign_entity",
                columns: table => new
                {
                    pvt_entity = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    foreign_entity = table.Column<int>(type: "int", nullable: false),
                    subscription = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pvt_entity_has_foreign_entity", x => new { x.pvt_entity, x.foreign_entity });
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_foreign_entity_foreign_entity_foreign_entity",
                        column: x => x.foreign_entity,
                        principalTable: "foreign_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_foreign_entity_private_entity_pvt_entity",
                        column: x => x.pvt_entity,
                        principalTable: "private_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_foreign_entity_subscription_subscription",
                        column: x => x.subscription,
                        principalTable: "subscription",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pvt_entity_has_pvt_entity",
                columns: table => new
                {
                    owner = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    owned = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    subscription = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pvt_entity_has_pvt_entity", x => new { x.owner, x.owned });
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_pvt_entity_private_entity_owned",
                        column: x => x.owned,
                        principalTable: "private_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_pvt_entity_private_entity_owner",
                        column: x => x.owner,
                        principalTable: "private_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_pvt_entity_subscription_subscription",
                        column: x => x.subscription,
                        principalTable: "subscription",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pvt_entity_has_subscriber",
                columns: table => new
                {
                    entity = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    subscriber = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    subscription = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pvt_entity_has_subscriber", x => new { x.entity, x.subscriber });
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_subscriber_private_entity_entity",
                        column: x => x.entity,
                        principalTable: "private_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_subscriber_private_entity_roles_role",
                        column: x => x.role,
                        principalTable: "private_entity_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_subscriber_subscriber_subscriber",
                        column: x => x.subscriber,
                        principalTable: "subscriber",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pvt_entity_has_subscriber_subscription_subscription",
                        column: x => x.subscription,
                        principalTable: "subscription",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_amended_article_article",
                table: "amended_article",
                column: "article");

            migrationBuilder.CreateIndex(
                name: "IX_application_city",
                table: "application",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "IX_application_service",
                table: "application",
                column: "service");

            migrationBuilder.CreateIndex(
                name: "IX_application_status",
                table: "application",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_application_task",
                table: "application",
                column: "task");

            migrationBuilder.CreateIndex(
                name: "CountryCode",
                table: "city",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_foreign_entity_country",
                table: "foreign_entity",
                column: "country");

            migrationBuilder.CreateIndex(
                name: "IX_memorandum_object_memorandum",
                table: "memorandum_object",
                column: "memorandum");

            migrationBuilder.CreateIndex(
                name: "IX_name_search_application",
                table: "name_search",
                column: "application");

            migrationBuilder.CreateIndex(
                name: "IX_name_search_designation",
                table: "name_search",
                column: "designation");

            migrationBuilder.CreateIndex(
                name: "IX_name_search_reason_for_search",
                table: "name_search",
                column: "reason_for_search");

            migrationBuilder.CreateIndex(
                name: "IX_name_search_ref",
                table: "name_search",
                column: "ref",
                unique: true,
                filter: "[ref] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_name_search_service",
                table: "name_search",
                column: "service");

            migrationBuilder.CreateIndex(
                name: "IX_office_city",
                table: "office",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "IX_private_entity_application",
                table: "private_entity",
                column: "application");

            migrationBuilder.CreateIndex(
                name: "IX_private_entity_articles",
                table: "private_entity",
                column: "articles");

            migrationBuilder.CreateIndex(
                name: "IX_private_entity_last_application",
                table: "private_entity",
                column: "last_application");

            migrationBuilder.CreateIndex(
                name: "IX_private_entity_LastApplicationServiceApplicationId",
                table: "private_entity",
                column: "LastApplicationServiceApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_private_entity_memorandum",
                table: "private_entity",
                column: "memorandum");

            migrationBuilder.CreateIndex(
                name: "IX_private_entity_name",
                table: "private_entity",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_private_entity_office",
                table: "private_entity",
                column: "office");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_foreign_entity_foreign_entity",
                table: "pvt_entity_has_foreign_entity",
                column: "foreign_entity");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_foreign_entity_pvt_entity",
                table: "pvt_entity_has_foreign_entity",
                column: "pvt_entity");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_foreign_entity_subscription",
                table: "pvt_entity_has_foreign_entity",
                column: "subscription");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_pvt_entity_owned",
                table: "pvt_entity_has_pvt_entity",
                column: "owned");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_pvt_entity_owner",
                table: "pvt_entity_has_pvt_entity",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_pvt_entity_subscription",
                table: "pvt_entity_has_pvt_entity",
                column: "subscription");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_subscriber_entity",
                table: "pvt_entity_has_subscriber",
                column: "entity");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_subscriber_role",
                table: "pvt_entity_has_subscriber",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_subscriber_subscriber",
                table: "pvt_entity_has_subscriber",
                column: "subscriber");

            migrationBuilder.CreateIndex(
                name: "IX_pvt_entity_has_subscriber_subscription",
                table: "pvt_entity_has_subscriber",
                column: "subscription");

            migrationBuilder.CreateIndex(
                name: "IX_service_department_id",
                table: "service",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscriber_gender",
                table: "subscriber",
                column: "gender");

            migrationBuilder.CreateIndex(
                name: "IX_suggested_name_name_search",
                table: "suggested_name",
                column: "name_search");

            migrationBuilder.CreateIndex(
                name: "IX_suggested_name_status",
                table: "suggested_name",
                column: "status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amended_article");

            migrationBuilder.DropTable(
                name: "memorandum_object");

            migrationBuilder.DropTable(
                name: "pvt_entity_has_foreign_entity");

            migrationBuilder.DropTable(
                name: "pvt_entity_has_pvt_entity");

            migrationBuilder.DropTable(
                name: "pvt_entity_has_subscriber");

            migrationBuilder.DropTable(
                name: "foreign_entity");

            migrationBuilder.DropTable(
                name: "private_entity");

            migrationBuilder.DropTable(
                name: "private_entity_roles");

            migrationBuilder.DropTable(
                name: "subscriber");

            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropTable(
                name: "article_of_association");

            migrationBuilder.DropTable(
                name: "memorandum_of_association");

            migrationBuilder.DropTable(
                name: "office");

            migrationBuilder.DropTable(
                name: "suggested_name");

            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "name_search");

            migrationBuilder.DropTable(
                name: "application");

            migrationBuilder.DropTable(
                name: "designation");

            migrationBuilder.DropTable(
                name: "reason_for_search");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "service");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
