using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FerminToroMS.Infrastructure.Migrations
{
    public partial class EstadoOnPagos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Pagos",
                type: "text",
                nullable: false,
                defaultValue: "No Conciliado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pagos");
        }
    }
}
