using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class LeadCommentFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "LeadAssignments",
                newName: "Rate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CommentedOn",
                table: "LeadAssignments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA1",
                columns: new[] { "ActionName", "ActionTarget" },
                values: new object[] { "CommentLead", "Rating" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentedOn",
                table: "LeadAssignments");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "LeadAssignments",
                newName: "Rating");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: "ALA1",
                columns: new[] { "ActionName", "ActionTarget" },
                values: new object[] { "Comment", "Message" });
        }
    }
}
