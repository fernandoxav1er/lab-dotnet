using Catalogo.API.Models;

namespace Catalogo.API.Interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<PagedList<Categoria>> ObterCategoriaPaginados(CategoriaParametersRequest paginationParameters);
}
