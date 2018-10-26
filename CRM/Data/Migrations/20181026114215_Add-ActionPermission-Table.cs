using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class AddActionPermissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionPermissions",
                columns: table => new
                {
                    ActionId = table.Column<string>(nullable: false),
                    ApplicationRoleName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPermissions", x => new { x.ActionId, x.ApplicationRoleName });
                    table.ForeignKey(
                        name: "FK_ActionPermissions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ActionPermissions",
                columns: new[] { "ActionId", "ApplicationRoleName" },
                values: new object[,]
                {
                    { "AL0", "Admin" },
                    { "ALA5", "Manager" },
                    { "ALA5", "Admin" },
                    { "ALA4", "Agent" },
                    { "ALA4", "Manager" },
                    { "ALA4", "Admin" },
                    { "ALA3", "Partner" },
                    { "ALA3", "Agent" },
                    { "ALA3", "Manager" },
                    { "ALA3", "Admin" },
                    { "ALA2", "Partner" },
                    { "ALA2", "Agent" },
                    { "ALA2", "Manager" },
                    { "ALA2", "Admin" },
                    { "ALA1", "Partner" },
                    { "ALA0", "Partner" },
                    { "ALA0", "Agent" },
                    { "ALA0", "Manager" },
                    { "ALA0", "Admin" },
                    { "AL3", "Agent" },
                    { "AL3", "Manager" },
                    { "AL3", "Admin" },
                    { "AL2", "Agent" },
                    { "AL2", "Manager" },
                    { "AL2", "Admin" },
                    { "AL1", "Agent" },
                    { "AL1", "Manager" },
                    { "AL1", "Admin" },
                    { "AL0", "Agent" },
                    { "AL0", "Manager" },
                    { "ALA5", "Agent" }
                });

            migrationBuilder.UpdateData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 1,
                column: "GST",
                value: 0.1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionPermissions");

            migrationBuilder.UpdateData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 1,
                column: "GST",
                value: 10.0);
        }
    }
}
