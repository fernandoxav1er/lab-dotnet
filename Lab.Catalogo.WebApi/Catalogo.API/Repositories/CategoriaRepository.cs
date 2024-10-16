using Catalogo.API.Context;
using Catalogo.API.Interfaces;
using Catalogo.API.Models;

namespace Catalogo.API.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context) { }

    public Task<PagedList<Categoria>> ObterCategoriaPaginados(CategoriaParametersRequest paginationParameters)
    {
        var categorias = GetAll().OrderBy(p => p.CategoriaId).AsQueryable();
        var categoriasOrdenadas = PagedList<Categoria>.ToPagedList(categorias, paginationParameters.PageNumber, paginationParameters.PageSize);
        return Task.FromResult(categoriasOrdenadas);
    }

    public Task<PagedList<Categoria>> ObterCategoriasFiltroNomePaginados(CategoriasFiltroNome categoriaParameters)
    {
        var categorias = GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(categoriaParameters.Nome))
            categorias = categorias.Where(p => p.Nome.Contains(categoriaParameters.Nome));

        var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias, categoriaParameters.PageNumber, categoriaParameters.PageSize);
        return Task.FromResult(categoriasFiltradas);
    }

}
