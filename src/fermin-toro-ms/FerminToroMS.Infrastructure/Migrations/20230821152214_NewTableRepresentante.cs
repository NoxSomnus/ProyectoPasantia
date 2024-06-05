using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class NewTableRepresentante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rango_Edad",
                table: "Estudiantes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "RepresentanteId",
                table: "Estudiantes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NroVacantes",
                table: "Cronogramas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Representantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellido = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representantes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_RepresentanteId",
                table: "Estudiantes",
                column: "RepresentanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estudiantes_Representantes_RepresentanteId",
                table: "Estudiantes",
                column: "RepresentanteId",
                principalTable: "Representantes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estudiantes_Representantes_RepresentanteId",
                table: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Representantes");

            migrationBuilder.DropIndex(
                name: "IX_Estudiantes_RepresentanteId",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "RepresentanteId",
                table: "Estudiantes");

            migrationBuilder.AlterColumn<string>(
                name: "Rango_Edad",
                table: "Estudiantes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NroVacantes",
                table: "Cronogramas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
