using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class InvoiceInvoiceItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "LeadType",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LeadAssignment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "LeadAssignment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Lead",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "GST",
                table: "Company",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    No = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    GST = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    InvoiceNo = table.Column<int>(nullable: false),
                    LeadAssignmentId = table.Column<int>(nullable: false),
                    Reinvoiced = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => new { x.InvoiceNo, x.LeadAssignmentId });
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice_InvoiceNo",
                        column: x => x.InvoiceNo,
                        principalTable: "Invoice",
                        principalColumn: "No",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_LeadAssignment_LeadAssignmentId",
                        column: x => x.LeadAssignmentId,
                        principalTable: "LeadAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_LeadAssignmentId",
                table: "InvoiceItem",
                column: "LeadAssignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "LeadType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LeadAssignment");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "LeadAssignment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "GST",
                table: "Company");
        }
    }
}
