using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class OfficeAddressPartnerBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "PartnerBranch");

            migrationBuilder.DropColumn(
                name: "State",
                table: "PartnerBranch");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "PartnerBranch");

            migrationBuilder.DropColumn(
                name: "Suburb",
                table: "PartnerBranch");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "Suburb",
                table: "Office");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "PartnerBranch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Office",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetAddress = table.Column<string>(maxLength: 256, nullable: true),
                    Suburb = table.Column<string>(maxLength: 64, nullable: true),
                    State = table.Column<string>(maxLength: 4, nullable: true),
                    PostCode = table.Column<string>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerBranch_AddressId",
                table: "PartnerBranch",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Office_AddressId",
                table: "Office",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Office_Address_AddressId",
                table: "Office",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerBranch_Address_AddressId",
                table: "PartnerBranch",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Office_Address_AddressId",
                table: "Office");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerBranch_Address_AddressId",
                table: "PartnerBranch");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_PartnerBranch_AddressId",
                table: "PartnerBranch");

            migrationBuilder.DropIndex(
                name: "IX_Office_AddressId",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "PartnerBranch");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Office");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "PartnerBranch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "PartnerBranch",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "PartnerBranch",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suburb",
                table: "PartnerBranch",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Office",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Office",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Office",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suburb",
                table: "Office",
                maxLength: 128,
                nullable: true);
        }
    }
}
