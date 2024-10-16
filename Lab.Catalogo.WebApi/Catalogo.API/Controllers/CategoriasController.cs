using AutoMapper;
using Catalogo.API.Interfaces;
using Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Catalogo.API.Controllers;

[Route("api/categorias")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public CategoriasController(ILogger<CategoriasController> logger,
                                       IUnitOfWork uof,
                                       IMapper mapper)
    {
        _logger = logger;
        _uof = uof;
        _mapper = mapper;
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

        var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoriaDto);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            _logger.LogWarning("Dados inválidos...");
            return BadRequest("Dados inválidos...");
        }

        var categoria = _mapper.Map<Categoria>(categoriaDto);

        var categoriaCriada = await _uof.CategoriaRepository.Create(categoria);
        await _uof.CommitAsync();

        var novaCategoriaDto = _mapper.Map<CategoriaDTO>(categoriaCriada);

        return CreatedAtAction("GetCategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId) return BadRequest();

        var categoria = _mapper.Map<Categoria>(categoriaDto);

        var categoriaAtualizada = await _uof.CategoriaRepository.Update(categoria);
        await _uof.CommitAsync();

        var categoriaAtualizadaDto = _mapper.Map<CategoriaDTO>(categoriaAtualizada);

        return Ok(categoriaAtualizadaDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoriaDTO>> Delete(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);
        if (categoria is null) return NotFound();

        var categoriaExcluida = await _uof.CategoriaRepository.Delete(categoria);
        await _uof.CommitAsync();

        var categoriaExcluidaDto = _mapper.Map<CategoriaDTO>(categoriaExcluida);

        return Ok(categoriaExcluidaDto);
    }

    [HttpGet("mapeamento-manual")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasManual()
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

    [HttpGet("mapeamento-metodos-extensao")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasExternal()
    {
        var categorias = await _uof.CategoriaRepository.GetAllAsync();

        if (!categorias.Any()) return NotFound();

        var categoriasDto = categorias.Select(c => c.ToCategoriaDTO()).ToList();

        return Ok(categoriasDto);
    }

    [HttpGet("mapeamento-automapper")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasAutoMapper()
    {
        var categorias = await _uof.CategoriaRepository.GetAllAsync();

        if (!categorias.Any()) return NotFound();

        var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

        return Ok(categoriasDto);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasPagination([FromQuery] CategoriaParametersRequest categoriaParameters)
    {
        var categorias = await _uof.CategoriaRepository.ObterCategoriaPaginados(categoriaParameters);

        var metadata = new
        {
            categorias.TotalCount,
            categorias.PageSize,
            categorias.CurrentPage,
            categorias.TotalPages,
            categorias.HasNext,
            categorias.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        // var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
        var categoriasDto = categorias.ToCategoriaDTOList();

        return Ok(categoriasDto);
    }


    [HttpGet("filter-categoria-nome-pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasByNome([FromQuery] CategoriasFiltroNome categoriaParameters)
    {
        var categorias = await _uof.CategoriaRepository.ObterCategoriasFiltroNomePaginados(categoriaParameters);

        var metadata = new
        {
            categorias.TotalCount,
            categorias.PageSize,
            categorias.CurrentPage,
            categorias.TotalPages,
            categorias.HasNext,
            categorias.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

        return Ok(categoriasDto);
    }
}
