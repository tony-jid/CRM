using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Removecreateddatetimeanduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadAssignmentStates",
                table: "LeadAssignmentStates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CreatedTimestamp",
                table: "LeadAssignmentStates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LeadAssignments");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "LeadAssignments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadAssignmentStates",
                table: "LeadAssignmentStates",
                columns: new[] { "StateId", "LeadAssignmentId", "ActionTimestamp" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadAssignmentStates",
                table: "LeadAssignmentStates");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Leads",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimestamp",
                table: "LeadAssignmentStates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LeadAssignments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "LeadAssignments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadAssignmentStates",
                table: "LeadAssignmentStates",
                columns: new[] { "StateId", "LeadAssignmentId", "CreatedTimestamp" });
        }
    }
}
