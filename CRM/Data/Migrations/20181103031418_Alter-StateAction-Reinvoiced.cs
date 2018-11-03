using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class AlterStateActionReinvoiced : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[] { "SLA5", "ALA0" });

            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[] { "SLA5", "ALA1" });

            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[] { "SLA5", "ALA5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA5", "ALA0" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA5", "ALA1" });

            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA5", "ALA5" });
        }
    }
}
