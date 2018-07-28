using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Lead_LeadAssignmentwithStatesforHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignment_State_StateId",
                table: "LeadAssignment");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "TransactionType");

            migrationBuilder.DropIndex(
                name: "IX_LeadAssignment_StateId",
                table: "LeadAssignment");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "LeadAssignment");

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "State",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "State",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Repeatable",
                table: "State",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NextStateId",
                table: "Action",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LeadAssignmentState",
                columns: table => new
                {
                    LeadAssignmentId = table.Column<int>(nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    Actor = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    ActionTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadAssignmentState", x => new { x.StateId, x.LeadAssignmentId, x.CreatedTimestamp });
                    table.ForeignKey(
                        name: "FK_LeadAssignmentState_LeadAssignment_LeadAssignmentId",
                        column: x => x.LeadAssignmentId,
                        principalTable: "LeadAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadAssignmentState_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadState",
                columns: table => new
                {
                    LeadId = table.Column<Guid>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false),
                    Actor = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    ActionTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadState", x => new { x.StateId, x.LeadId, x.CreatedTimestamp });
                    table.ForeignKey(
                        name: "FK_LeadState_Lead_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Lead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeadState_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_NextStateId",
                table: "Action",
                column: "NextStateId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignmentState_LeadAssignmentId",
                table: "LeadAssignmentState",
                column: "LeadAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadState_LeadId",
                table: "LeadState",
                column: "LeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Action_State_NextStateId",
                table: "Action",
                column: "NextStateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Action_State_NextStateId",
                table: "Action");

            migrationBuilder.DropTable(
                name: "LeadAssignmentState");

            migrationBuilder.DropTable(
                name: "LeadState");

            migrationBuilder.DropIndex(
                name: "IX_Action_NextStateId",
                table: "Action");

            migrationBuilder.DropColumn(
                name: "Repeatable",
                table: "State");

            migrationBuilder.DropColumn(
                name: "NextStateId",
                table: "Action");

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "State",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "State",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "LeadAssignment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TransactionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionName = table.Column<string>(nullable: false),
                    ControllerName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NextTransactionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionType_TransactionType_NextTransactionTypeId",
                        column: x => x.NextTransactionTypeId,
                        principalTable: "TransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Lead_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Lead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignment_StateId",
                table: "LeadAssignment",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_LeadId",
                table: "Transaction",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionTypeId",
                table: "Transaction",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionType_NextTransactionTypeId",
                table: "TransactionType",
                column: "NextTransactionTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignment_State_StateId",
                table: "LeadAssignment",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
