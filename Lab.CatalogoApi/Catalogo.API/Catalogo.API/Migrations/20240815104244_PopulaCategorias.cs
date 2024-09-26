using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.API.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categorias (Nome, ImagemmUrl) VALUES ('Bebidas', 'bebidas.png')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemmUrl) VALUES ('Lanches', 'lanches.png')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemmUrl) VALUES ('Sobremesas', 'sobremesas.png')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
        }
    }
}