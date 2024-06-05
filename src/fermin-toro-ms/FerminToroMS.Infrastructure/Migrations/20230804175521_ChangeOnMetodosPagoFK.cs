using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnMetodosPagoFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MetodoPagoId",
                table: "Pagos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_MetodoPagoId",
                table: "Pagos",
                column: "MetodoPagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Metodos_Pago_MetodoPagoId",
                table: "Pagos",
                column: "MetodoPagoId",
                principalTable: "Metodos_Pago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Metodos_Pago_MetodoPagoId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_MetodoPagoId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "MetodoPagoId",
                table: "Pagos");
        }
    }
}
