using FinShark.API.Data;
using FinShark.API.Dtos.Estoque;
using FinShark.API.Helpers;
using FinShark.API.Interfaces;
using FinShark.API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace FinShark.API.Controllers
{
    [Authorize]
    [Route("api/estoque")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        public readonly ApplicationDBContext _context;
        private readonly IEstoqueRepository _estoqueRepository;
        public EstoqueController(ApplicationDBContext context, 
                                 IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
            _context = context;
        }
        
        [SwaggerOperation(Summary ="Retorna todos os registros de forma assíncrona", Description = "Get")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var estoque = await _estoqueRepository.GetAll(query);
            var estoqueDtos = estoque.Select(s => s.ToEstoqueDto());
            return Ok(estoqueDtos);
        } 
        
        [SwaggerOperation(Summary ="Retorna registros paginados", Description = "Get")]
        [HttpGet("pagination")]
        public async Task<IActionResult> GetPagination([FromQuery] QueryPagination query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var estoque = await _estoqueRepository.GetPagination(query);
            var estoqueDtos = estoque.Select(s => s.ToEstoqueDto());
            return Ok(estoqueDtos);
        }

        [SwaggerOperation(Summary = "Retorna o registro passado na rota de forma assíncrona", Description = "Get")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var estoque = await _estoqueRepository.GetById(id);

            if (estoque == null) return NotFound();

            return Ok(estoque.ToEstoqueDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarEstoqueRequestDto estoqueDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var estoqueModel = estoqueDTO.ToEstoqueCreateDto();

            await _estoqueRepository.Create(estoqueModel);
            
            return CreatedAtAction(nameof(GetById), new { id = estoqueModel.Id }, estoqueModel.ToEstoqueDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AtualizarEstoqueRequestDto atualizarDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var estoqueModel = await _estoqueRepository.Update(id, atualizarDto);
            
            if (estoqueModel == null) return NotFound();

            return Ok(estoqueModel.ToEstoqueDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var estoqueModel = await _estoqueRepository.Delete(id);

            if (estoqueModel == null) return NotFound();

            return NoContent();
        }
    }
}
