using Catalogo.API.Context;
using Catalogo.API.Interfaces;
using Catalogo.API.Models;

namespace Catalogo.API.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context) { }

    public Task<PagedList<Categoria>> ObterCategoriaPaginados(CategoriaParametersRequest paginationParameters)
    {
        var categorias = GetAllPagination().OrderBy(p => p.CategoriaId).AsQueryable();
        var categoriasOrdenadas = PagedList<Categoria>.ToPagedList(categorias, paginationParameters.PageNumber, paginationParameters.PageSize);
        return Task.FromResult(categoriasOrdenadas);
    }
}
