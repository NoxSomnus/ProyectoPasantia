using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnInscripcionCantidad_A_Pagardefaultvalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadAPagar",
                table: "Inscripciones");

            migrationBuilder.AddColumn<double>(
                name: "Cantidad_A_Pagar",
                table: "Inscripciones",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad_A_Pagar",
                table: "Inscripciones");

            migrationBuilder.AddColumn<double>(
                name: "CantidadAPagar",
                table: "Inscripciones",
                type: "double precision",
                nullable: true);
        }
    }
}
