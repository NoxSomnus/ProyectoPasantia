using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnModuloDiminutivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diminutivo",
                table: "Modulos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diminutivo",
                table: "Modulos");
        }
    }
}
