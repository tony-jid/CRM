using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Addactionprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionType",
                table: "Actions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestType",
                table: "Actions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "Actions");
        }
    }
}
