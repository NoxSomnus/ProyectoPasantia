using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class NewTablePagosAprobados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cronogramas_Precios_PrecioId",
                table: "Cronogramas");

            migrationBuilder.DropIndex(
                name: "IX_Cronogramas_PrecioId",
                table: "Cronogramas");

            migrationBuilder.DropColumn(
                name: "PrecioId",
                table: "Cronogramas");

            migrationBuilder.AddColumn<double>(
                name: "CantidadAPagar",
                table: "Inscripciones",
                type: "double precision",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pagos_Aprobados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NroTransaccion = table.Column<int>(type: "integer", nullable: true),
                    FechaTransaccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: true),
                    NroCuenta = table.Column<int>(type: "integer", nullable: false),
                    Nombre_Empelado = table.Column<string>(type: "text", nullable: false),
                    FechaConciliacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PagoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos_Aprobados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagos_Aprobados_Pagos_PagoId",
                        column: x => x.PagoId,
                        principalTable: "Pagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_Aprobados_PagoId",
                table: "Pagos_Aprobados",
                column: "PagoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos_Aprobados");

            migrationBuilder.DropColumn(
                name: "CantidadAPagar",
                table: "Inscripciones");

            migrationBuilder.AddColumn<Guid>(
                name: "PrecioId",
                table: "Cronogramas",
                type: "uuid",
                nullable: true);

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
        }
    }
}
