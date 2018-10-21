using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class AlterStatesActionsOptimized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL4");

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SL2", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SL3", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA1", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA2", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA3", "AL1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA4", "AL1" });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL1",
                columns: new[] { "ActionName", "DisplayName", "NextStateId" },
                values: new object[] { "SendLeadRequestInfo", "Request Info", "SL4" });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "ActionName", "ActionTarget", "ControllerName", "DisplayName", "Icon", "NextStateId", "RequestType" },
                values: new object[] { "AL0", "SendLeadMessage", "Message", "Message", "Send message", "batch-icon batch-icon-envelope", "S0", "Post" });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "ActionName", "ActionTarget", "ControllerName", "DisplayName", "Icon", "NextStateId", "RequestType" },
                values: new object[] { "ALA0", "SendAssignmentMessage", "Message", "Message", "Send message", "batch-icon batch-icon-envelope", "S0", "Post" });

            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[,]
                {
                    { "SL1", "AL0" },
                    { "SL4", "AL0" },
                    { "SL2", "AL0" },
                    { "SL3", "AL0" },
                    { "SLA1", "ALA0" },
                    { "SLA2", "ALA0" },
                    { "SLA3", "ALA0" },
                    { "SLA4", "ALA0" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SL1", "AL0" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SL2", "AL0" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SL3", "AL0" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SL4", "AL0" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA1", "ALA0" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA2", "ALA0" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA3", "ALA0" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA4", "ALA0" });

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL0");

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA0");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "AL1",
                columns: new[] { "ActionName", "DisplayName", "NextStateId" },
                values: new object[] { "Send", "Send message", "S0" });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "ActionName", "ActionTarget", "ControllerName", "DisplayName", "Icon", "NextStateId", "RequestType" },
                values: new object[] { "AL4", "RequestInfo", "Message", "Message", "Request Info", "batch-icon batch-icon-envelope", "SL4", "Post" });

            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[,]
                {
                    { "SL2", "AL1" },
                    { "SL3", "AL1" },
                    { "SLA1", "AL1" },
                    { "SLA2", "AL1" },
                    { "SLA3", "AL1" },
                    { "SLA4", "AL1" }
                });
        }
    }
}
