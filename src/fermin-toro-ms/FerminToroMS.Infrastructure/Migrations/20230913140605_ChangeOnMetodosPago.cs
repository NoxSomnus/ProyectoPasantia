using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnMetodosPago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Metodos_Pago",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cuenta",
                table: "Metodos_Pago",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreDe",
                table: "Metodos_Pago",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rif",
                table: "Metodos_Pago",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Metodos_Pago",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Metodos_Pago");

            migrationBuilder.DropColumn(
                name: "Cuenta",
                table: "Metodos_Pago");

            migrationBuilder.DropColumn(
                name: "NombreDe",
                table: "Metodos_Pago");

            migrationBuilder.DropColumn(
                name: "Rif",
                table: "Metodos_Pago");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Metodos_Pago");
        }
    }
}
