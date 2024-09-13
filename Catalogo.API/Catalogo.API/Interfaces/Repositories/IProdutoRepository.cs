using Catalogo.API.Models;

namespace Catalogo.API.Interfaces.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> GetProdutosPorCategoria(int id);
}
