using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class StateAction_LeadAssignment_StateAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Partner",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControllerName = table.Column<string>(nullable: true),
                    ActionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Owner = table.Column<string>(nullable: true),
                    Seq = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadAssignment",
                columns: table => new
                {
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<Guid>(nullable: false),
                    DeleteDateTime = table.Column<DateTime>(nullable: false),
                    DeleteUserId = table.Column<Guid>(nullable: false),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    UpdateUserId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeadId = table.Column<Guid>(nullable: false),
                    PartnerBranchId = table.Column<Guid>(nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadAssignment_Lead_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Lead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadAssignment_PartnerBranch_PartnerBranchId",
                        column: x => x.PartnerBranchId,
                        principalTable: "PartnerBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadAssignment_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StateAction",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false),
                    ActionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateAction", x => new { x.StateId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_StateAction_Action_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Action",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StateAction_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignment_LeadId",
                table: "LeadAssignment",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignment_PartnerBranchId",
                table: "LeadAssignment",
                column: "PartnerBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignment_StateId",
                table: "LeadAssignment",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_StateAction_ActionId",
                table: "StateAction",
                column: "ActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadAssignment");

            migrationBuilder.DropTable(
                name: "StateAction");

            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Partner");
        }
    }
}
