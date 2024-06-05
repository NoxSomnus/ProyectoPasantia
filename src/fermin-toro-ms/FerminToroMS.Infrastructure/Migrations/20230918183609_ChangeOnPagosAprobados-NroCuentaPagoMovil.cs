using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnPagosAprobadosNroCuentaPagoMovil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NroCuenta",
                table: "Pagos_Aprobados");

            migrationBuilder.RenameColumn(
                name: "TelefonoPagoMovil",
                table: "Pagos_Aprobados",
                newName: "NroCuentaPagoMovil");

            migrationBuilder.AlterColumn<string>(
                name: "NroTransaccion",
                table: "Pagos_Aprobados",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NroCuentaPagoMovil",
                table: "Pagos_Aprobados",
                newName: "TelefonoPagoMovil");

            migrationBuilder.AlterColumn<string>(
                name: "NroTransaccion",
                table: "Pagos_Aprobados",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "NroCuenta",
                table: "Pagos_Aprobados",
                type: "text",
                nullable: true);
        }
    }
}
