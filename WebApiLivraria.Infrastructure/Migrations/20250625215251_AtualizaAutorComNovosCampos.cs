using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLivraria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaAutorComNovosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Autores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Biografia",
                table: "Autores",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Autores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Autores",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalNascimento",
                table: "Autores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biografia",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "LocalNascimento",
                table: "Autores");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Autores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
