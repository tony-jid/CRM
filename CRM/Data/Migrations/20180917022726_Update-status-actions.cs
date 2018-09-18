using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Updatestatusactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Actions",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL1",
                column: "Icon",
                value: "batch-icon batch-icon-envelope");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL2",
                column: "Icon",
                value: "batch-icon batch-icon-user-alt-2");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL3",
                column: "Icon",
                value: "batch-icon batch-icon-user-alt-2");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA1",
                column: "Icon",
                value: "batch-icon batch-speech-bubble-left-tip-solid-text");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA2",
                column: "Icon",
                value: "batch-icon batch-icon-tick");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA3",
                column: "Icon",
                value: "fa fa-dollar");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                column: "Icon",
                value: "batch-icon batch-icon-cross");

            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[,]
                {
                    { "SLA1", "AL1" },
                    { "SLA1", "ALA1" },
                    { "SLA1", "ALA2" },
                    { "SLA1", "ALA3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA1", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA1", "ALA1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA1", "ALA2" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA1", "ALA3" });

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "Actions",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL1",
                column: "Icon",
                value: "envelope");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL2",
                column: "Icon",
                value: "user-alt-2");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL3",
                column: "Icon",
                value: "user-alt-2");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA1",
                column: "Icon",
                value: "");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA2",
                column: "Icon",
                value: "");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA3",
                column: "Icon",
                value: "");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                column: "Icon",
                value: "");
        }
    }
}
