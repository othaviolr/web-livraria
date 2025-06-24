using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLivraria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSinopseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sinopses",
                columns: table => new
                {
                    LivroId = table.Column<int>(type: "int", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinopses", x => x.LivroId);
                    table.ForeignKey(
                        name: "FK_Sinopses_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_EditoraId",
                table: "Livros",
                column: "EditoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Editoras_EditoraId",
                table: "Livros",
                column: "EditoraId",
                principalTable: "Editoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Editoras_EditoraId",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "Sinopses");

            migrationBuilder.DropIndex(
                name: "IX_Livros_EditoraId",
                table: "Livros");
        }
    }
}
