using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class LeadAssignmentaddComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "LeadAssignment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "LeadAssignment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "LeadAssignment");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "LeadAssignment");
        }
    }
}
