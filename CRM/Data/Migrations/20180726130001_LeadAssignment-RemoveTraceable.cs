using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class LeadAssignmentRemoveTraceable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "LeadAssignment");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "LeadAssignment");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "LeadAssignment");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "LeadAssignment");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "LeadAssignment");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "LeadAssignment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "LeadAssignment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "LeadAssignment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateTime",
                table: "LeadAssignment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "LeadAssignment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "LeadAssignment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "LeadAssignment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
