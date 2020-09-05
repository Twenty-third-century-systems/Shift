using Microsoft.EntityFrameworkCore.Migrations;

namespace Shish.Migrations
{
    public partial class Policy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Policy",
                table: "PersonalInfo");

            migrationBuilder.AddColumn<string>(
                name: "Policy",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Policy",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Policy",
                table: "PersonalInfo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
