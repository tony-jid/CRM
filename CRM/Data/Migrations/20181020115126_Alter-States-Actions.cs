using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class AlterStatesActions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                column: "ActionName",
                value: "SendInvoice");

            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[,]
                {
                    { "SLA2", "AL1" },
                    { "SLA2", "ALA1" },
                    { "SLA2", "ALA3" },
                    { "SLA2", "ALA4" },
                    { "SLA3", "AL1" },
                    { "SLA3", "ALA1" },
                    { "SLA3", "ALA2" },
                    { "SLA3", "ALA4" },
                    { "SLA4", "AL1" },
                    { "SLA4", "ALA1" },
                    { "SLA4", "ALA5" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name", "Owner", "Repeatable", "Seq" },
                values: new object[] { "SL4", "Requested Info", "Lead", false, 1 });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "ActionName", "ActionTarget", "ControllerName", "DisplayName", "Icon", "NextStateId", "RequestType" },
                values: new object[] { "AL4", "RequestInfo", "Message", "Message", "Request Info", "batch-icon batch-icon-envelope", "SL4", "Post" });

            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[] { "SL4", "AL1" });

            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[] { "SL4", "AL2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL4");

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SL4", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SL4", "AL2" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA2", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA2", "ALA1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA2", "ALA3" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA2", "ALA4" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA3", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA3", "ALA1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA3", "ALA2" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA3", "ALA4" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA4", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA4", "ALA1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA4", "ALA5" });

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: "SL4");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA5",
                column: "ActionName",
                value: "ResendInvoice");
        }
    }
}
