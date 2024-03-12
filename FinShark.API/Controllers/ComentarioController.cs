using FinShark.API.Dtos.Comentario;
using FinShark.API.Interfaces;
using FinShark.API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.API.Controllers
{
    [Route("api/comentario")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioRepository _commentRepository;
        private readonly IEstoqueRepository _estoqueRepository;
        public ComentarioController(IComentarioRepository comentarioRepository, IEstoqueRepository estoqueRepository)
        {
            _commentRepository = comentarioRepository;
            _estoqueRepository = estoqueRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Essa instrução faz valer as anotações de validação nos DTOS
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comentarios = await _commentRepository.GetAllAsync();
            var comentarioDto = comentarios.Select(s => s.ToComentarioDto());
            return Ok(comentarioDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // Essa instrução faz valer as anotações de validação nos DTOS
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comentario = await _commentRepository.GetByIdAsync(id);

            if (comentario == null)
                return NotFound();

            return Ok(comentario.ToComentarioDto());
        }

        [HttpPost("{estoqueId:int}")]
        public async Task<IActionResult> Create([FromRoute] int estoqueId, CriarComentarioDto comentDto )
        {
            // Essa instrução faz valer as anotações de validação nos DTOS
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _estoqueRepository.EstoqueExist(estoqueId))
                return BadRequest("Não existe o estoque informado");

            var comentarioModel = comentDto.ToComentarioCreateDto(estoqueId);
            await _commentRepository.CreateAsync(comentarioModel);
            return CreatedAtAction(nameof(GetById), new {id = comentarioModel}, comentarioModel.ToComentarioDto());

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // Essa instrução faz valer as anotações de validação nos DTOS
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepository.DeleteAsync(id);
            
            if (commentModel == null)
                return NotFound("Não existe este comentário");

            return NoContent();
        }
       
    }
}
