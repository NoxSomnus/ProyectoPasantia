using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnPermiso_EmpleadoFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisos_Empleados_EmpleadoEntityId",
                table: "Permisos");

            migrationBuilder.DropIndex(
                name: "IX_Permisos_EmpleadoEntityId",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "EmpleadoEntityId",
                table: "Permisos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmpleadoEntityId",
                table: "Permisos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_EmpleadoEntityId",
                table: "Permisos",
                column: "EmpleadoEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisos_Empleados_EmpleadoEntityId",
                table: "Permisos",
                column: "EmpleadoEntityId",
                principalTable: "Empleados",
                principalColumn: "Id");
        }
    }
}
