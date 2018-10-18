using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class AlterCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Customers_CustomerId",
                table: "Leads");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Company",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.UpdateData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "leads@comparisonadvantage.com.au");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Customers_CustomerId",
                table: "Leads",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Customers_CustomerId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Company");

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
                name: "FK_Leads_Customers_CustomerId",
                table: "Leads",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
