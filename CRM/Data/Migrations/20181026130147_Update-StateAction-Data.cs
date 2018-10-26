using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdateStateActionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StateActions",
                keyColumns: new[] { "StateId", "ActionId" },
                keyValues: new object[] { "SLA3", "ALA4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[] { "SLA3", "ALA4" });
        }
    }
}
