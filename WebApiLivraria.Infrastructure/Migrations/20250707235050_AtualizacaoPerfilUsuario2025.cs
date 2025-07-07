using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLivraria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoPerfilUsuario2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId1",
                table: "Leituras",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId1",
                table: "Favoritos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_UsuarioId1",
                table: "Leituras",
                column: "UsuarioId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Leituras_Usuarios_UsuarioId1",
                table: "Leituras",
                column: "UsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favoritos_Usuarios_UsuarioId1",
                table: "Favoritos");

            migrationBuilder.DropForeignKey(
                name: "FK_Leituras_Usuarios_UsuarioId1",
                table: "Leituras");

            migrationBuilder.DropIndex(
                name: "IX_Leituras_UsuarioId1",
                table: "Leituras");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_UsuarioId1",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Favoritos");
        }
    }
}
