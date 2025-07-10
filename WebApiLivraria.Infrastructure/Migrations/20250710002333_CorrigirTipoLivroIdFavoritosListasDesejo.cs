using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLivraria.Infrastructure.Migrations
{
    public partial class CorrigirTipoLivroIdFavoritosListasDesejo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // FAVORITOS
            migrationBuilder.DropForeignKey(
                name: "FK_Favoritos_Livros_LivroId",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "Favoritos");

            migrationBuilder.AddColumn<int>(
                name: "LivroId",
                table: "Favoritos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritos_Livros_LivroId",
                table: "Favoritos",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // LISTAS DESEJO
            migrationBuilder.DropForeignKey(
                name: "FK_ListasDesejo_Livros_LivroId",
                table: "ListasDesejo");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "ListasDesejo");

            migrationBuilder.AddColumn<int>(
                name: "LivroId",
                table: "ListasDesejo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ListasDesejo_Livros_LivroId",
                table: "ListasDesejo",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // FAVORITOS
            migrationBuilder.DropForeignKey(
                name: "FK_Favoritos_Livros_LivroId",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "Favoritos");

            migrationBuilder.AddColumn<Guid>(
                name: "LivroId",
                table: "Favoritos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritos_Livros_LivroId",
                table: "Favoritos",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // LISTAS DESEJO
            migrationBuilder.DropForeignKey(
                name: "FK_ListasDesejo_Livros_LivroId",
                table: "ListasDesejo");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "ListasDesejo");

            migrationBuilder.AddColumn<Guid>(
                name: "LivroId",
                table: "ListasDesejo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddForeignKey(
                name: "FK_ListasDesejo_Livros_LivroId",
                table: "ListasDesejo",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
