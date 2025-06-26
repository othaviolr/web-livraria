using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiLivraria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposBiografiaEImagemNaEditora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biografia",
                table: "Editoras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Editoras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biografia",
                table: "Editoras");

            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Editoras");
        }
    }
}
