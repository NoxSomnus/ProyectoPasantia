using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class NewColumnsOnPagosAprobados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre_Empelado",
                table: "Pagos_Aprobados",
                newName: "Nombre_Empleado");

            migrationBuilder.AlterColumn<string>(
                name: "NroTransaccion",
                table: "Pagos_Aprobados",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NroCuenta",
                table: "Pagos_Aprobados",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "ComprobanteIVA",
                table: "Pagos_Aprobados",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TasaBCV",
                table: "Pagos_Aprobados",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelefonoPagoMovil",
                table: "Pagos_Aprobados",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComprobanteIVA",
                table: "Pagos_Aprobados");

            migrationBuilder.DropColumn(
                name: "TasaBCV",
                table: "Pagos_Aprobados");

            migrationBuilder.DropColumn(
                name: "TelefonoPagoMovil",
                table: "Pagos_Aprobados");

            migrationBuilder.RenameColumn(
                name: "Nombre_Empleado",
                table: "Pagos_Aprobados",
                newName: "Nombre_Empelado");

            migrationBuilder.AlterColumn<int>(
                name: "NroTransaccion",
                table: "Pagos_Aprobados",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NroCuenta",
                table: "Pagos_Aprobados",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
