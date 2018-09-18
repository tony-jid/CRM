using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Correctcontrollernamesinactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA1",
                columns: new[] { "ControllerName", "Icon" },
                values: new object[] { "LeadAssignments", "batch-icon batch-icon-speech-bubble-left-tip-text" });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA2",
                column: "ControllerName",
                value: "LeadAssignments");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA3",
                column: "ControllerName",
                value: "LeadAssignments");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA4",
                column: "ControllerName",
                value: "LeadAssignments");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                column: "ControllerName",
                value: "LeadAssignments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA1",
                columns: new[] { "ControllerName", "Icon" },
                values: new object[] { "LeadAssigments", "batch-icon batch-icon-speech-bubble-left-tip-solid-text" });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA2",
                column: "ControllerName",
                value: "LeadAssigments");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA3",
                column: "ControllerName",
                value: "LeadAssigments");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA4",
                column: "ControllerName",
                value: "LeadAssigments");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                column: "ControllerName",
                value: "LeadAssigments");
        }
    }
}
