using Microsoft.EntityFrameworkCore.Migrations;

namespace Expressway.Database.Migrations
{
    public partial class foreign_key_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_DriverVehicles_VehicleId_DriverId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_VehicleId_DriverId",
                table: "Rides");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DriverId_VehicleId",
                table: "Rides",
                columns: new[] { "DriverId", "VehicleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_DriverVehicles_DriverId_VehicleId",
                table: "Rides",
                columns: new[] { "DriverId", "VehicleId" },
                principalTable: "DriverVehicles",
                principalColumns: new[] { "DriverId", "VehicleId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_DriverVehicles_DriverId_VehicleId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_DriverId_VehicleId",
                table: "Rides");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_VehicleId_DriverId",
                table: "Rides",
                columns: new[] { "VehicleId", "DriverId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_DriverVehicles_VehicleId_DriverId",
                table: "Rides",
                columns: new[] { "VehicleId", "DriverId" },
                principalTable: "DriverVehicles",
                principalColumns: new[] { "DriverId", "VehicleId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
