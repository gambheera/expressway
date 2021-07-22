using Microsoft.EntityFrameworkCore.Migrations;

namespace Expressway.Database.Migrations
{
    public partial class seat_plan_template_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypes_SeatPlanTemplate_SeatPlanTemplateId",
                table: "VehicleTypes");

            migrationBuilder.DropIndex(
                name: "IX_VehicleTypes_SeatPlanTemplateId",
                table: "VehicleTypes");

            migrationBuilder.DropColumn(
                name: "SeatPlanTemplateId",
                table: "VehicleTypes");

            migrationBuilder.AddColumn<long>(
                name: "SeatPlanTemplateId",
                table: "VehicleModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_SeatPlanTemplateId",
                table: "VehicleModels",
                column: "SeatPlanTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_SeatPlanTemplate_SeatPlanTemplateId",
                table: "VehicleModels",
                column: "SeatPlanTemplateId",
                principalTable: "SeatPlanTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_SeatPlanTemplate_SeatPlanTemplateId",
                table: "VehicleModels");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModels_SeatPlanTemplateId",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "SeatPlanTemplateId",
                table: "VehicleModels");

            migrationBuilder.AddColumn<long>(
                name: "SeatPlanTemplateId",
                table: "VehicleTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleTypes_SeatPlanTemplateId",
                table: "VehicleTypes",
                column: "SeatPlanTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypes_SeatPlanTemplate_SeatPlanTemplateId",
                table: "VehicleTypes",
                column: "SeatPlanTemplateId",
                principalTable: "SeatPlanTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
