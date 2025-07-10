using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLivraria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarRelacionamentosFavoritosListasLeituras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favoritos_Usuarios_UsuarioId1",
                table: "Favoritos");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_UsuarioId1",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Favoritos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId1",
                table: "Favoritos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UsuarioId1",
                table: "Favoritos",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritos_Usuarios_UsuarioId1",
                table: "Favoritos",
                column: "UsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
