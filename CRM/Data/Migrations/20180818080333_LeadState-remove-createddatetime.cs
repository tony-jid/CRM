using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class LeadStateremovecreateddatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadStates",
                table: "LeadStates");

            migrationBuilder.DropColumn(
                name: "CreatedTimestamp",
                table: "LeadStates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadStates",
                table: "LeadStates",
                columns: new[] { "StateId", "LeadId", "ActionTimestamp" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadStates",
                table: "LeadStates");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimestamp",
                table: "LeadStates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadStates",
                table: "LeadStates",
                columns: new[] { "StateId", "LeadId", "CreatedTimestamp" });
        }
    }
}
