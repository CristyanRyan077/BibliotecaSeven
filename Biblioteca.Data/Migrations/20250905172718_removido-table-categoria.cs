using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Data.Migrations
{
    /// <inheritdoc />
    public partial class removidotablecategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Categorias_CategoriaId",
                table: "Livros");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Livros_CategoriaId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_ISBN",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Livros");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Livros",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Livros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Livros",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_CategoriaId",
                table: "Livros",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_ISBN",
                table: "Livros",
                column: "ISBN",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Categorias_CategoriaId",
                table: "Livros",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id");
        }
    }
}
