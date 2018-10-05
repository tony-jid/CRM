using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class UpdateMessageTemplateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "MessageTemplates");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "MessageTemplates");

            migrationBuilder.AddColumn<string>(
                name: "MessageBody",
                table: "MessageTemplates",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MessageSubject",
                table: "MessageTemplates",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageBody",
                table: "MessageTemplates");

            migrationBuilder.DropColumn(
                name: "MessageSubject",
                table: "MessageTemplates");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "MessageTemplates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "MessageTemplates",
                nullable: true);
        }
    }
}
