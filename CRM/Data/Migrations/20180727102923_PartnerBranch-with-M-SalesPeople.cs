using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class PartnerBranchwithMSalesPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "PartnerBranch");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PartnerBranch");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Suburb",
                table: "Customer");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SalesPerson",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContactName = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    EMail = table.Column<string>(nullable: false),
                    BranchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesPerson_PartnerBranch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "PartnerBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressId",
                table: "Customer",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPerson_BranchId",
                table: "SalesPerson",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "SalesPerson");

            migrationBuilder.DropIndex(
                name: "IX_Customer_AddressId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "PartnerBranch",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PartnerBranch",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Customer",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Customer",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suburb",
                table: "Customer",
                maxLength: 128,
                nullable: true);
        }
    }
}
