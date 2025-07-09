using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLivraria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirConversaoStatusLeitura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leituras_Usuarios_UsuarioId1",
                table: "Leituras");

            migrationBuilder.DropIndex(
                name: "IX_Leituras_UsuarioId1",
                table: "Leituras");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Leituras");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId1",
                table: "Leituras",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_UsuarioId1",
                table: "Leituras",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Leituras_Usuarios_UsuarioId1",
                table: "Leituras",
                column: "UsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
