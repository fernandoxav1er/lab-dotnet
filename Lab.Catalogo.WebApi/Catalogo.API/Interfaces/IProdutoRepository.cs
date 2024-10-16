using Catalogo.API.Models;

namespace Catalogo.API.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> GetProdutosPorCategoria(int id);
    Task<IEnumerable<Produto>> ObterProdutosPaginados(ProdutosParametersRequest produtosParameters);
    Task<PagedList<Produto>> ObterProdutosPaginadosPaged(ProdutosParametersRequest paginationParameters);
    Task<PagedList<Produto>> ObterProdutosFiltroPrecoPaginados(ProdutosFiltroPreco produtosParameters);
}
