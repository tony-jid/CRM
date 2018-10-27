using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class AlterGroupActionDisplay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL0",
                column: "GroupActionDisplayName",
                value: "Message customers");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL1",
                column: "GroupActionDisplayName",
                value: "Request info");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA0",
                column: "GroupActionDisplayName",
                value: "Message partners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL0",
                column: "GroupActionDisplayName",
                value: "Send customers message");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL1",
                column: "GroupActionDisplayName",
                value: "Request customers info");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA0",
                column: "GroupActionDisplayName",
                value: "Send partners message");
        }
    }
}
