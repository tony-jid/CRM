using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Alterinvoiceaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL4",
                columns: new[] { "ActionName", "ActionTarget", "ControllerName", "RequestType" },
                values: new object[] { "InvoiceByLeads", "Window", "Reports", "Get" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL4",
                columns: new[] { "ActionName", "ActionTarget", "ControllerName", "RequestType" },
                values: new object[] { "GetInvoices", "Ajax", "Leads", "Post" });
        }
    }
}
