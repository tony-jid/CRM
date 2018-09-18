using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Updatestatusactions1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA1",
                column: "Icon",
                value: "batch-icon batch-icon-speech-bubble-left-tip-solid-text");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA3",
                column: "Icon",
                value: "batch-icon batch-icon-cross");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA4",
                column: "Icon",
                value: "fa fa-dollar");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                column: "Icon",
                value: "fa fa-dollar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA1",
                column: "Icon",
                value: "batch-icon batch-speech-bubble-left-tip-solid-text");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA3",
                column: "Icon",
                value: "fa fa-dollar");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA4",
                column: "Icon",
                value: "");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                column: "Icon",
                value: "batch-icon batch-icon-cross");
        }
    }
}
