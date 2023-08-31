using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnInscriptionsFechaInscripcion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EstadoSolvencia",
                table: "Inscripciones",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInscripcion",
                table: "Inscripciones",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaInscripcion",
                table: "Inscripciones");

            migrationBuilder.AlterColumn<string>(
                name: "EstadoSolvencia",
                table: "Inscripciones",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
