using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnPrecios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Precios_Cursos_CursoEntityId",
                table: "Precios");

            migrationBuilder.DropIndex(
                name: "IX_Precios_CursoEntityId",
                table: "Precios");

            migrationBuilder.DropColumn(
                name: "CursoEntityId",
                table: "Precios");

            migrationBuilder.RenameColumn(
                name: "CursoId",
                table: "Precios",
                newName: "ModuloId");

            migrationBuilder.AddColumn<Guid>(
                name: "PrecioId",
                table: "Cronogramas",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Precios_ModuloId",
                table: "Precios",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Cronogramas_PrecioId",
                table: "Cronogramas",
                column: "PrecioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cronogramas_Precios_PrecioId",
                table: "Cronogramas",
                column: "PrecioId",
                principalTable: "Precios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Precios_Modulos_ModuloId",
                table: "Precios",
                column: "ModuloId",
                principalTable: "Modulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cronogramas_Precios_PrecioId",
                table: "Cronogramas");

            migrationBuilder.DropForeignKey(
                name: "FK_Precios_Modulos_ModuloId",
                table: "Precios");

            migrationBuilder.DropIndex(
                name: "IX_Precios_ModuloId",
                table: "Precios");

            migrationBuilder.DropIndex(
                name: "IX_Cronogramas_PrecioId",
                table: "Cronogramas");

            migrationBuilder.DropColumn(
                name: "PrecioId",
                table: "Cronogramas");

            migrationBuilder.RenameColumn(
                name: "ModuloId",
                table: "Precios",
                newName: "CursoId");

            migrationBuilder.AddColumn<Guid>(
                name: "CursoEntityId",
                table: "Precios",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Precios_CursoEntityId",
                table: "Precios",
                column: "CursoEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Precios_Cursos_CursoEntityId",
                table: "Precios",
                column: "CursoEntityId",
                principalTable: "Cursos",
                principalColumn: "Id");
        }
    }
}
