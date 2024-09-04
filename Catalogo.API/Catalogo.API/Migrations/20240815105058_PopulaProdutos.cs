using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.API.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql(@"INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)
                    VALUES
                    ('Suco de Laranja', 'Suco de laranja natural 500ml', 5.99, 'https://example.com/images/suco-laranja.jpg', 100.0, '2024-08-15 10:00:00', 1),
                    ('Refrigerante Cola', 'Refrigerante sabor cola 350ml', 4.49, 'https://example.com/images/refrigerante-cola.jpg', 150.0, '2024-08-15 11:00:00', 1),
                    ('Hambúrguer Clássico', 'Hambúrguer com carne, queijo, alface e tomate', 14.99, 'https://example.com/images/hamburguer-classico.jpg', 50.0, '2024-08-15 12:00:00', 2),
                    ('Sanduíche Natural', 'Sanduíche natural com frango desfiado e salada', 9.99, 'https://example.com/images/sanduiche-natural.jpg', 80.0, '2024-08-15 13:00:00', 2),
                    ('Torta de Limão', 'Torta de limão com base crocante e recheio cremoso', 6.99, 'https://example.com/images/torta-limao.jpg', 40.0, '2024-08-15 14:00:00', 3);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}