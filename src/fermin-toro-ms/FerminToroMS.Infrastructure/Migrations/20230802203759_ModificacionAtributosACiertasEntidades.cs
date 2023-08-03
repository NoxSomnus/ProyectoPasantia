using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ModificacionAtributosACiertasEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cronogramas_Empleados_InstructorId",
                table: "Cronogramas");

            migrationBuilder.DropColumn(
                name: "URLInfo",
                table: "Metodos_Pago");

            migrationBuilder.DropColumn(
                name: "Regularidad",
                table: "Inscripciones");

            migrationBuilder.RenameColumn(
                name: "Nota",
                table: "Inscripciones",
                newName: "NotaAcademica");

            migrationBuilder.AlterColumn<string>(
                name: "FechaPagoEfectivo",
                table: "Pagos",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "EsPagoDeAbono",
                table: "Pagos",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Fecha_Nac",
                table: "Estudiantes",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo_Verificacion",
                table: "Estudiantes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Estudiantes",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InstructorId",
                table: "Cronogramas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaFin",
                table: "Cronogramas",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "Regularidad",
                table: "Cronogramas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cronogramas_Empleados_InstructorId",
                table: "Cronogramas",
                column: "InstructorId",
                principalTable: "Empleados",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cronogramas_Empleados_InstructorId",
                table: "Cronogramas");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "Regularidad",
                table: "Cronogramas");

            migrationBuilder.RenameColumn(
                name: "NotaAcademica",
                table: "Inscripciones",
                newName: "Nota");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaPagoEfectivo",
                table: "Pagos",
                type: "date",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "EsPagoDeAbono",
                table: "Pagos",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URLInfo",
                table: "Metodos_Pago",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Regularidad",
                table: "Inscripciones",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Fecha_Nac",
                table: "Estudiantes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo_Verificacion",
                table: "Estudiantes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InstructorId",
                table: "Cronogramas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaFin",
                table: "Cronogramas",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cronogramas_Empleados_InstructorId",
                table: "Cronogramas",
                column: "InstructorId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
