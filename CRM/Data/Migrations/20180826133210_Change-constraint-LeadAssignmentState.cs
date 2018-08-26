using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ChangeconstraintLeadAssignmentState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentStates_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentStates");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentStates_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentStates",
                column: "LeadAssignmentId",
                principalTable: "LeadAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentStates_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentStates");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentStates_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentStates",
                column: "LeadAssignmentId",
                principalTable: "LeadAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
