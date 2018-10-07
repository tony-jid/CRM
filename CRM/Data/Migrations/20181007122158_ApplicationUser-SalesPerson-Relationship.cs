using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ApplicationUserSalesPersonRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPeople_AspNetUsers_AspNetUserId",
                table: "SalesPeople");

            migrationBuilder.DropIndex(
                name: "IX_SalesPeople_AspNetUserId",
                table: "SalesPeople");

            migrationBuilder.RenameColumn(
                name: "AspNetUserId",
                table: "SalesPeople",
                newName: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPeople_ApplicationUserId",
                table: "SalesPeople",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPeople_AspNetUsers_ApplicationUserId",
                table: "SalesPeople",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesPeople_AspNetUsers_ApplicationUserId",
                table: "SalesPeople");

            migrationBuilder.DropIndex(
                name: "IX_SalesPeople_ApplicationUserId",
                table: "SalesPeople");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "SalesPeople",
                newName: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPeople_AspNetUserId",
                table: "SalesPeople",
                column: "AspNetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPeople_AspNetUsers_AspNetUserId",
                table: "SalesPeople",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
