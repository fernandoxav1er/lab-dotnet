using Catalogo.API.Context;
using Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.API.Controllersl;

[Route("api/produtos")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public ProdutosController(AppDbContext context, ILogger<ProdutosController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        //_logger.LogInformation("============ ProdutosController - registrando logger ===========");

        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null) return NotFound("Produto não encontrado");

        return produto;
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {
        if (!ModelState.IsValid) return BadRequest();

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduto", new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutProduto(int id, Produto produto)
    {
        if (id != produto.ProdutoId) return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(produto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);

        if (produto == null) return NotFound("Produto não localizado");

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return Ok(produto);
    }
}