using Catalogo.API.Interfaces.Repositories;

namespace Catalogo.API.Interfaces;

public interface IUnitOfWork
{
    IProdutoRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    Task CommitAsync();
}
