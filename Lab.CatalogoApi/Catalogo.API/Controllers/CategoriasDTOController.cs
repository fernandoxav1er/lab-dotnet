using Catalogo.API.Controllersl;
using Catalogo.API.DTOs;
using Catalogo.API.DTOs.Mappings;
using Catalogo.API.Interfaces;
using Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers;

[Route("api/categorias-dto")]
[ApiController]
public class CategoriasDTOController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _uof;

    public CategoriasDTOController(ILogger<ProdutosController> logger,
                                IUnitOfWork uof)
    {
        _logger = logger;
        _uof = uof;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
    {
        var categorias = await _uof.CategoriaRepository.GetAllAsync();

        if (!categorias.Any()) return NotFound();

        var categoriasDto = new List<CategoriaDTO>();

        foreach (var categoria in categorias)
        {
            var categoriaDto = new CategoriaDTO()
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemmUrl = categoria.ImagemmUrl
            };
            categoriasDto.Add(categoriaDto);
        }

        return Ok(categoriasDto);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning("Categoria não encontrada");
            return NotFound("Categoria não encontrada");
        }

        //var categoriaDTO = new CategoriaDTO()
        //{
        //    CategoriaId = categoria.CategoriaId,
        //    Nome = categoria.Nome,
        //    ImagemmUrl = categoria.ImagemmUrl
        //};

        return Ok(categoria.ToCategoiaDTO());
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            _logger.LogWarning("Dados inválidos...");
            return BadRequest("Dados inválidos...");
        }

        //var categoria = new Categoria()
        //{
        //    ImagemmUrl= categoriaDto.ImagemmUrl,
        //    Nome = categoriaDto.Nome,
        //    CategoriaId = categoriaDto.CategoriaId
        //};

        var categoria = categoriaDto.ToCategoria();

        var categoriaCriada = await _uof.CategoriaRepository.Create(categoria);
        await _uof.CommitAsync();

        var novaCategoriaDto = new CategoriaDTO()
        {
            ImagemmUrl = categoriaCriada.ImagemmUrl,
            Nome = categoriaCriada.Nome,
            CategoriaId = categoriaCriada.CategoriaId
        };

        return CreatedAtAction("GetCategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId) return BadRequest();

        var categoria = new Categoria()
        {
            ImagemmUrl = categoriaDto.ImagemmUrl,
            Nome = categoriaDto.Nome,
            CategoriaId = categoriaDto.CategoriaId
        };

        var categoriaAtualizada = await _uof.CategoriaRepository.Update(categoria);
        await _uof.CommitAsync();

        var categoriaAtualizadaDto = new CategoriaDTO()
        {
            ImagemmUrl = categoriaAtualizada.ImagemmUrl,
            Nome = categoriaAtualizada.Nome,
            CategoriaId = categoriaAtualizada.CategoriaId
        };

        return Ok(categoriaAtualizadaDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoriaDTO>> Delete(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);
        if (categoria is null) return NotFound();

        var categoriaExcluida = await _uof.CategoriaRepository.Delete(categoria);
        await _uof.CommitAsync();

        var categoriaExcluidaDto = new CategoriaDTO()
        {
            ImagemmUrl = categoriaExcluida.ImagemmUrl,
            Nome = categoriaExcluida.Nome,
            CategoriaId = categoriaExcluida.CategoriaId
        };

        return Ok(categoriaExcluidaDto);
    }
}