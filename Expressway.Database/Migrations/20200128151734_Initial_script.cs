using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Expressway.Database.Migrations
{
    public partial class Initial_script : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangePoints",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Latitude = table.Column<long>(nullable: false),
                    Longitude = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangePoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileNoVerifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MobileNo = table.Column<string>(nullable: true),
                    DeviceUniqueId = table.Column<string>(nullable: true),
                    HasVerificationCodeRequested = table.Column<bool>(nullable: false),
                    VerificationCodeRequestedTime = table.Column<DateTime>(nullable: false),
                    HasVerificationCodeSentSuccessfully = table.Column<bool>(nullable: false),
                    VerificationCodeSuccessfullySentTime = table.Column<DateTime>(nullable: false),
                    SentVerificationCode = table.Column<string>(nullable: true),
                    HasMobileNoVerifiedSuccessfully = table.Column<bool>(nullable: false),
                    MobileNoSuccessfullyVerifiedTime = table.Column<DateTime>(nullable: false),
                    VerificationUniqueIdGivenByMobile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileNoVerifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatPlanTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlanJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatPlanTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Id = table.Column<long>(nullable: false),
                    AuthKey = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsAccountSuspended = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserId);
                    table.UniqueConstraint("AK_UserLogins_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleBrands",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SeatPlanTemplateId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleTypes_SeatPlanTemplate_SeatPlanTemplateId",
                        column: x => x.SeatPlanTemplateId,
                        principalTable: "SeatPlanTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    MobileNo = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    UserMode = table.Column<int>(nullable: false),
                    Nic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_MobileNo", x => x.MobileNo);
                    table.ForeignKey(
                        name: "FK_Users_UserLogins_Id",
                        column: x => x.Id,
                        principalTable: "UserLogins",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleBrandId = table.Column<long>(nullable: false),
                    VehicleTypeId = table.Column<long>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModels_VehicleBrands_VehicleBrandId",
                        column: x => x.VehicleBrandId,
                        principalTable: "VehicleBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleModels_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegisterNumber = table.Column<string>(nullable: false),
                    VehicleModelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.UniqueConstraint("AK_Vehicles_RegisterNumber", x => x.RegisterNumber);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleModels_VehicleModelId",
                        column: x => x.VehicleModelId,
                        principalTable: "VehicleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverVehicles",
                columns: table => new
                {
                    DriverId = table.Column<long>(nullable: false),
                    VehicleId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverVehicles", x => new { x.DriverId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_DriverVehicles_Users_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DriverId = table.Column<long>(nullable: false),
                    VehicleId = table.Column<long>(nullable: false),
                    StartingTime = table.Column<DateTime>(nullable: false),
                    EntryPointId = table.Column<long>(nullable: false),
                    ExitPointId = table.Column<long>(nullable: false),
                    AproximateTimeDuration = table.Column<int>(nullable: false),
                    RideState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rides_ExchangePoints_EntryPointId",
                        column: x => x.EntryPointId,
                        principalTable: "ExchangePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rides_ExchangePoints_ExitPointId",
                        column: x => x.ExitPointId,
                        principalTable: "ExchangePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rides_DriverVehicles_VehicleId_DriverId",
                        columns: x => new { x.VehicleId, x.DriverId },
                        principalTable: "DriverVehicles",
                        principalColumns: new[] { "DriverId", "VehicleId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverRatingsByPassenger",
                columns: table => new
                {
                    DriverId = table.Column<long>(nullable: false),
                    RideId = table.Column<long>(nullable: false),
                    PassengerId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverRatingsByPassenger", x => new { x.DriverId, x.PassengerId, x.RideId });
                    table.ForeignKey(
                        name: "FK_DriverRatingsByPassenger_Users_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DriverRatingsByPassenger_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverRatingsByPassenger_Users_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DriverRatingsByPassenger_Rides_RideId",
                        column: x => x.RideId,
                        principalTable: "Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PassengerRatingsByDriver",
                columns: table => new
                {
                    PassengerId = table.Column<long>(nullable: false),
                    RideId = table.Column<long>(nullable: false),
                    DriverId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerRatingsByDriver", x => new { x.PassengerId, x.DriverId, x.RideId });
                    table.ForeignKey(
                        name: "FK_PassengerRatingsByDriver_Users_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PassengerRatingsByDriver_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassengerRatingsByDriver_Users_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PassengerRatingsByDriver_Rides_RideId",
                        column: x => x.RideId,
                        principalTable: "Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    RideId = table.Column<long>(nullable: false),
                    RowIndex = table.Column<int>(nullable: false),
                    IsLeftCorner = table.Column<bool>(nullable: false),
                    IsRightCorner = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => new { x.Id, x.RideId });
                    table.ForeignKey(
                        name: "FK_Seats_Rides_RideId",
                        column: x => x.RideId,
                        principalTable: "Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassengerRideRequests",
                columns: table => new
                {
                    SeatId = table.Column<long>(nullable: false),
                    RideId = table.Column<long>(nullable: false),
                    PassengerId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    RequestStatus = table.Column<int>(nullable: false),
                    RequestedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerRideRequests", x => new { x.SeatId, x.RideId, x.PassengerId });
                    table.ForeignKey(
                        name: "FK_PassengerRideRequests_Users_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PassengerRideRequests_Seats_SeatId_RideId",
                        columns: x => new { x.SeatId, x.RideId },
                        principalTable: "Seats",
                        principalColumns: new[] { "Id", "RideId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverRatingsByPassenger_Id",
                table: "DriverRatingsByPassenger",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRatingsByPassenger_PassengerId",
                table: "DriverRatingsByPassenger",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRatingsByPassenger_RideId",
                table: "DriverRatingsByPassenger",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverVehicles_VehicleId",
                table: "DriverVehicles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerRatingsByDriver_DriverId",
                table: "PassengerRatingsByDriver",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerRatingsByDriver_Id",
                table: "PassengerRatingsByDriver",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerRatingsByDriver_RideId",
                table: "PassengerRatingsByDriver",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_PassengerRideRequests_PassengerId",
                table: "PassengerRideRequests",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_EntryPointId",
                table: "Rides",
                column: "EntryPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_ExitPointId",
                table: "Rides",
                column: "ExitPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_VehicleId_DriverId",
                table: "Rides",
                columns: new[] { "VehicleId", "DriverId" });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_RideId",
                table: "Seats",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleBrandId",
                table: "VehicleModels",
                column: "VehicleBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleTypeId",
                table: "VehicleModels",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleModelId",
                table: "Vehicles",
                column: "VehicleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_SeatPlanTemplateId",
                table: "VehicleTypes",
                column: "SeatPlanTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverRatingsByPassenger");

            migrationBuilder.DropTable(
                name: "MobileNoVerifications");

            migrationBuilder.DropTable(
                name: "PassengerRatingsByDriver");

            migrationBuilder.DropTable(
                name: "PassengerRideRequests");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "ExchangePoints");

            migrationBuilder.DropTable(
                name: "DriverVehicles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropTable(
                name: "VehicleBrands");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropTable(
                name: "SeatPlanTemplate");
        }
    }
}
