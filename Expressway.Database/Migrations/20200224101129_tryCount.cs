using Microsoft.EntityFrameworkCore.Migrations;

namespace Expressway.Database.Migrations
{
    public partial class tryCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraMobileNo",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuspended",
                table: "MobileNoVerifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TryCount",
                table: "MobileNoVerifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraMobileNo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsSuspended",
                table: "MobileNoVerifications");

            migrationBuilder.DropColumn(
                name: "TryCount",
                table: "MobileNoVerifications");
        }
    }
}
