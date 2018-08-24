using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class Newrelationpartnerservices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadTypes_Partners_PartnerId",
                table: "LeadTypes");

            migrationBuilder.DropIndex(
                name: "IX_LeadTypes_PartnerId",
                table: "LeadTypes");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "LeadTypes");

            migrationBuilder.CreateTable(
                name: "PartnerServices",
                columns: table => new
                {
                    PartnerId = table.Column<Guid>(nullable: false),
                    LeadTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerServices", x => new { x.PartnerId, x.LeadTypeId });
                    table.ForeignKey(
                        name: "FK_PartnerServices_LeadTypes_LeadTypeId",
                        column: x => x.LeadTypeId,
                        principalTable: "LeadTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerServices_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerServices_LeadTypeId",
                table: "PartnerServices",
                column: "LeadTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerServices");

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                table: "LeadTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadTypes_PartnerId",
                table: "LeadTypes",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadTypes_Partners_PartnerId",
                table: "LeadTypes",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
