using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLivraria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InicialComEditoraId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Editoras",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Biografia",
                table: "Editoras",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EditoraId",
                table: "Autores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Autores_EditoraId",
                table: "Autores",
                column: "EditoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autores_Editoras_EditoraId",
                table: "Autores",
                column: "EditoraId",
                principalTable: "Editoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autores_Editoras_EditoraId",
                table: "Autores");

            migrationBuilder.DropIndex(
                name: "IX_Autores_EditoraId",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "EditoraId",
                table: "Autores");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Editoras",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Biografia",
                table: "Editoras",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);
        }
    }
}
