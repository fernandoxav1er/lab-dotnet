using FinShark.API.Data;
using FinShark.API.Dtos.Estoque;
using FinShark.API.Interfaces;
using FinShark.API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace FinShark.API.Controllers
{
    [Route("api/estoque")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        public readonly ApplicationDBContext _context;
        private readonly IEstoqueRepository _estoqueRepository;
        public EstoqueController(ApplicationDBContext context, IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
            _context = context;
        }
        
        [SwaggerOperation(Summary ="Retorna todos os registros de forma assíncrona", Description = "Get")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Essa instrução faz valer as anotações de validação nos DTOS
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estoque = await _estoqueRepository.GetAllAsync();
            var estoqueDtos = estoque.Select(s => s.ToEstoqueDto());
            return Ok(estoqueDtos);
        }

        [SwaggerOperation(Summary = "Retorna o registro passado na rota de forma assíncrona", Description = "Get")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id) 
        {
            // Essa instrução faz valer as anotações de validação nos DTOS
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estoque = await _estoqueRepository.GetByIdAsync(id);

            if (estoque == null)
                return NotFound();

            return Ok(estoque.ToEstoqueDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CriarEstoqueRequestDto estoqueDTO)
        {
            // Essa instrução faz valer as anotações de validação nos DTOS
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estoqueModel = estoqueDTO.ToEstoqueCreateDto();

            await _estoqueRepository.CreateAsync(estoqueModel);
            
            return CreatedAtAction(nameof(GetByIdAsync), new { id = estoqueModel.Id }, estoqueModel.ToEstoqueDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] AtualizarEstoqueRequestDto atualizarDto)
        {
            // Essa instrução faz valer as anotações de validação nos DTOS
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estoqueModel = await _estoqueRepository.UpdateAsync(id, atualizarDto);
            
            if (estoqueModel == null)
                return NotFound();

            return Ok(estoqueModel.ToEstoqueDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id) 
        {
            // Essa instrução faz valer as anotações de validação nos DTOS
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estoqueModel = await _estoqueRepository.DeleteAsync(id);

            if (estoqueModel == null)
                return NotFound();

            return NoContent();
        }
    }
}
