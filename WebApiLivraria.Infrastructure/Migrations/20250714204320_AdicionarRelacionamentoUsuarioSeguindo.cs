using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLivraria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarRelacionamentoUsuarioSeguindo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuariosSeguindo",
                columns: table => new
                {
                    SeguidorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeguindoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosSeguindo", x => new { x.SeguidorId, x.SeguindoId });
                    table.ForeignKey(
                        name: "FK_UsuariosSeguindo_Usuarios_SeguidorId",
                        column: x => x.SeguidorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosSeguindo_Usuarios_SeguindoId",
                        column: x => x.SeguindoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSeguindo_SeguindoId",
                table: "UsuariosSeguindo",
                column: "SeguindoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosSeguindo");
        }
    }
}
