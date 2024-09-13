using Catalogo.API.Interfaces;
using Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllersl;

[Route("api/produtos")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public ProdutosController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        var produtos = await _uof.ProdutoRepository.GetAllAsync();
        if (produtos is null) return NotFound();
        return Ok(produtos);
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _uof.ProdutoRepository.GetAsync(c => c.ProdutoId == id);
        if (produto == null) return NotFound("Produto não encontrado");
        return produto;
    }

    [HttpGet("produtos-categoria/{id}")]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosCategoria (int id)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosPorCategoria(id);
        if (produtos is null) return NotFound();
        return Ok(produtos);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {
        if (produto is null) return BadRequest();

        var novoProduto = await _uof.ProdutoRepository.Create(produto);
        await _uof.CommitAsync();
        
        return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutProduto(int id, Produto produto)
    {
        if (id != produto.ProdutoId) return BadRequest();

        var produtoAtualizado = await _uof.ProdutoRepository.Update(produto);
        await _uof.CommitAsync();

        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);
        if (produto is null) return NotFound();

        var produtoDeletado = await _uof.ProdutoRepository.Delete(produto);
        await _uof.CommitAsync();

        return Ok($"Produto de id={id} foi excluído");
    }
}