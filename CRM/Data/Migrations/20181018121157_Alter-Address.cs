using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class AlterAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerBranches_Addresses_AddressId",
                table: "PartnerBranches");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Addresses",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "Addresses",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerBranches_Addresses_AddressId",
                table: "PartnerBranches",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerBranches_Addresses_AddressId",
                table: "PartnerBranches");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Addresses",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "Addresses",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerBranches_Addresses_AddressId",
                table: "PartnerBranches",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
