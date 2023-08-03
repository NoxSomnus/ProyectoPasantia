using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnCronogramas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Meses",
                table: "Periodos",
                newName: "MesInicio");

            migrationBuilder.AddColumn<string>(
                name: "MesFin",
                table: "Periodos",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MesFin",
                table: "Periodos");

            migrationBuilder.RenameColumn(
                name: "MesInicio",
                table: "Periodos",
                newName: "Meses");
        }
    }
}
