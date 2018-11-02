using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Alterinvoiceaction1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA4",
                columns: new[] { "ActionName", "ActionTarget", "ControllerName", "DisplayName", "RequestType" },
                values: new object[] { "InvoiceByAssignments", "Window", "Reports", "Get invoice", "Get" });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                columns: new[] { "ActionName", "ActionTarget", "ControllerName", "DisplayName", "RequestType" },
                values: new object[] { "InvoiceByAssignments", "Window", "Reports", "Get invoice", "Get" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA4",
                columns: new[] { "ActionName", "ActionTarget", "ControllerName", "DisplayName", "RequestType" },
                values: new object[] { "SendInvoice", "Ajax", "LeadAssignments", "Send invoice", "Post" });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                columns: new[] { "ActionName", "ActionTarget", "ControllerName", "DisplayName", "RequestType" },
                values: new object[] { "SendInvoice", "Ajax", "LeadAssignments", "Re-send invoice", "Post" });
        }
    }
}
