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
}
