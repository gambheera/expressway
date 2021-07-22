using Microsoft.EntityFrameworkCore.Migrations;

namespace Expressway.Database.Migrations
{
    public partial class available_seat_count_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxAvailableSeatCount",
                table: "VehicleModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeatCount",
                table: "Rides",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxAvailableSeatCount",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "AvailableSeatCount",
                table: "Rides");
        }
    }
}
