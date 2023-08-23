using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnInscripcionesEstudiantePago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Monto",
                table: "Pagos",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "EstudianteId",
                table: "Inscripciones",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_Nac",
                table: "Estudiantes",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_EstudianteId",
                table: "Inscripciones",
                column: "EstudianteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Estudiantes_EstudianteId",
                table: "Inscripciones",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Estudiantes_EstudianteId",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_EstudianteId",
                table: "Inscripciones");

            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "EstudianteId",
                table: "Inscripciones");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Fecha_Nac",
                table: "Estudiantes",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
