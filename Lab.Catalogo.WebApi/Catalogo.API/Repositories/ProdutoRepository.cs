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

    public Task<PagedList<Produto>> ObterProdutosFiltroPrecoPaginados(ProdutosFiltroPreco produtosFiltroParams)
    {
        var produtos = GetAll().AsQueryable();

        if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
        {
            if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
        }
        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);
        return Task.FromResult(produtosFiltrados);
    }


    public async Task<IEnumerable<Produto>> ObterProdutosPaginados(ProdutosParametersRequest produtosParameters)
    {
        return await GetAll()
            .OrderBy(p => p.Nome)
            .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
            .Take(produtosParameters.PageSize)
            .ToListAsync();
    }

    public Task<PagedList<Produto>> ObterProdutosPaginadosPaged(ProdutosParametersRequest paginationParameters)
    {
        var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, paginationParameters.PageNumber, paginationParameters.PageSize);
        return Task.FromResult(produtosOrdenados);
    }

}
