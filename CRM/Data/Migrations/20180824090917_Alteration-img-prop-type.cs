using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Alterationimgproptype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentState_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentState");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentState_States_StateId",
                table: "LeadAssignmentState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadAssignmentState",
                table: "LeadAssignmentState");

            migrationBuilder.RenameTable(
                name: "LeadAssignmentState",
                newName: "LeadAssignmentStates");

            migrationBuilder.RenameIndex(
                name: "IX_LeadAssignmentState_LeadAssignmentId",
                table: "LeadAssignmentStates",
                newName: "IX_LeadAssignmentStates_LeadAssignmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Partners",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                table: "LeadTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Company",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadAssignmentStates",
                table: "LeadAssignmentStates",
                columns: new[] { "StateId", "LeadAssignmentId", "CreatedTimestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_LeadTypes_PartnerId",
                table: "LeadTypes",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentStates_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentStates",
                column: "LeadAssignmentId",
                principalTable: "LeadAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentStates_States_StateId",
                table: "LeadAssignmentStates",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadTypes_Partners_PartnerId",
                table: "LeadTypes",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentStates_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentStates");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentStates_States_StateId",
                table: "LeadAssignmentStates");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadTypes_Partners_PartnerId",
                table: "LeadTypes");

            migrationBuilder.DropIndex(
                name: "IX_LeadTypes_PartnerId",
                table: "LeadTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadAssignmentStates",
                table: "LeadAssignmentStates");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "LeadTypes");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "LeadAssignmentStates",
                newName: "LeadAssignmentState");

            migrationBuilder.RenameIndex(
                name: "IX_LeadAssignmentStates_LeadAssignmentId",
                table: "LeadAssignmentState",
                newName: "IX_LeadAssignmentState_LeadAssignmentId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "Partners",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadAssignmentState",
                table: "LeadAssignmentState",
                columns: new[] { "StateId", "LeadAssignmentId", "CreatedTimestamp" });

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentState_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentState",
                column: "LeadAssignmentId",
                principalTable: "LeadAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentState_States_StateId",
                table: "LeadAssignmentState",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
