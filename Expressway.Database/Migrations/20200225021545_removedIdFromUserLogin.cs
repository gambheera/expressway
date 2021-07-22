using Microsoft.EntityFrameworkCore.Migrations;

namespace Expressway.Database.Migrations
{
    public partial class removedIdFromUserLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserLogins");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "UserLogins",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
