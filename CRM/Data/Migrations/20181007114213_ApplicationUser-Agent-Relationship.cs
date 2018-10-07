using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ApplicationUserAgentRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "SalesPeople",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Agents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPeople_AspNetUserId",
                table: "SalesPeople",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ApplicationUserId",
                table: "Agents",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AspNetUsers_ApplicationUserId",
                table: "Agents",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPeople_AspNetUsers_AspNetUserId",
                table: "SalesPeople",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_ApplicationUserId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesPeople_AspNetUsers_AspNetUserId",
                table: "SalesPeople");

            migrationBuilder.DropIndex(
                name: "IX_SalesPeople_AspNetUserId",
                table: "SalesPeople");

            migrationBuilder.DropIndex(
                name: "IX_Agents_ApplicationUserId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "SalesPeople");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Agents");
        }
    }
}
