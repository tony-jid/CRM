using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class AlterActionsAddGroupActionFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupActionDisplayName",
                table: "Actions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGroupAction",
                table: "Actions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL0",
                columns: new[] { "GroupActionDisplayName", "IsGroupAction" },
                values: new object[] { "Send customers message", true });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL1",
                columns: new[] { "GroupActionDisplayName", "IsGroupAction" },
                values: new object[] { "Request customers info", true });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA0",
                columns: new[] { "GroupActionDisplayName", "IsGroupAction" },
                values: new object[] { "Send partners message", true });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "ActionName", "ActionTarget", "ControllerName", "DisplayName", "GroupActionDisplayName", "Icon", "IsGroupAction", "NextStateId", "RequestType" },
                values: new object[] { "AL4", "GetInvoices", "Ajax", "Leads", "Get invoices", "Get invoices", "fa fa-dollar", true, "SLA4", "Post" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL4");

            migrationBuilder.DropColumn(
                name: "GroupActionDisplayName",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "IsGroupAction",
                table: "Actions");
        }
    }
}
