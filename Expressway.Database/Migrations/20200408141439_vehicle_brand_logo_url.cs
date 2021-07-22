using Microsoft.EntityFrameworkCore.Migrations;

namespace Expressway.Database.Migrations
{
    public partial class vehicle_brand_logo_url : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengerRideRequests_Seats_SeatId_RideId",
                table: "PassengerRideRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PassengerRideRequests",
                table: "PassengerRideRequests");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "VehicleBrands",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SeatId",
                table: "PassengerRideRequests",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "RequestingSeatCount",
                table: "PassengerRideRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "SeatRideId",
                table: "PassengerRideRequests",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassengerRideRequests",
                table: "PassengerRideRequests",
                columns: new[] { "RideId", "PassengerId" });

            migrationBuilder.CreateIndex(
                name: "IX_PassengerRideRequests_SeatId_SeatRideId",
                table: "PassengerRideRequests",
                columns: new[] { "SeatId", "SeatRideId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerRideRequests_Rides_RideId",
                table: "PassengerRideRequests",
                column: "RideId",
                principalTable: "Rides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerRideRequests_Seats_SeatId_SeatRideId",
                table: "PassengerRideRequests",
                columns: new[] { "SeatId", "SeatRideId" },
                principalTable: "Seats",
                principalColumns: new[] { "Id", "RideId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassengerRideRequests_Rides_RideId",
                table: "PassengerRideRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PassengerRideRequests_Seats_SeatId_SeatRideId",
                table: "PassengerRideRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PassengerRideRequests",
                table: "PassengerRideRequests");

            migrationBuilder.DropIndex(
                name: "IX_PassengerRideRequests_SeatId_SeatRideId",
                table: "PassengerRideRequests");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "VehicleBrands");

            migrationBuilder.DropColumn(
                name: "RequestingSeatCount",
                table: "PassengerRideRequests");

            migrationBuilder.DropColumn(
                name: "SeatRideId",
                table: "PassengerRideRequests");

            migrationBuilder.AlterColumn<long>(
                name: "SeatId",
                table: "PassengerRideRequests",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassengerRideRequests",
                table: "PassengerRideRequests",
                columns: new[] { "SeatId", "RideId", "PassengerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PassengerRideRequests_Seats_SeatId_RideId",
                table: "PassengerRideRequests",
                columns: new[] { "SeatId", "RideId" },
                principalTable: "Seats",
                principalColumns: new[] { "Id", "RideId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
