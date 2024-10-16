using Catalogo.API.Context;
using Catalogo.API.Interfaces;
using Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.API.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
    {
        var produtos = await _appDbContext.Produtos.ToListAsync();
        return produtos.Where(c => c.CategoriaId == id);
    }

    public async Task<IEnumerable<Produto>> ObterProdutosPaginados(ProdutosParametersRequest produtosParameters)
    {
        return await GetAllPagination()
            .OrderBy(p => p.Nome)
            .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            .Take(produtosParameters.PageSize)
            .ToListAsync();
    }

    public Task<PagedList<Produto>> ObterProdutosPaginadosPaged(ProdutosParametersRequest paginationParameters)
    {
        var produtos = GetAllPagination().OrderBy(p => p.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, paginationParameters.PageNumber, paginationParameters.PageSize);
        return Task.FromResult(produtosOrdenados);
    }

}
