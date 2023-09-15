using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class ChangeOnPagoEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsAprobado",
                table: "Pagos");

            migrationBuilder.AlterColumn<string>(
                name: "HoraPagoEfectivo",
                table: "Pagos",
                type: "text",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Pagos",
                type: "text",
                nullable: false,
                defaultValue: "No Comprobado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pagos");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "HoraPagoEfectivo",
                table: "Pagos",
                type: "time without time zone",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsAprobado",
                table: "Pagos",
                type: "boolean",
                nullable: true);
        }
    }
}
