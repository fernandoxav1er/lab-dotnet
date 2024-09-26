using FinShark.API.Dtos.Comentario;
using FinShark.API.Interfaces;
using FinShark.API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.API.Controllers
{
    [Authorize]
    [Route("api/comentario")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioRepository _commentRepository;
        private readonly IEstoqueRepository _estoqueRepository;
        public ComentarioController(IComentarioRepository comentarioRepository, 
                                    IEstoqueRepository estoqueRepository)
        {
            _commentRepository = comentarioRepository;
            _estoqueRepository = estoqueRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comentarios = await _commentRepository.GetAll();
            var comentarioDto = comentarios.Select(s => s.ToComentarioDto());
            return Ok(comentarioDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comentario = await _commentRepository.GetById(id);

            if (comentario == null) return NotFound();

            return Ok(comentario.ToComentarioDto());
        }

        [HttpPost("{estoqueId:int}")]
        [ActionName(nameof(GetById))]
        public async Task<IActionResult> Create([FromRoute] int estoqueId, CriarComentarioDto comentDto )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _estoqueRepository.EstoqueExist(estoqueId))
                return BadRequest("Não existe o estoque informado");

            var comentarioModel = comentDto.ToComentarioCreateDto(estoqueId);
            await _commentRepository.Create(comentarioModel);
            return CreatedAtAction(nameof(GetById), new {id = comentarioModel}, comentarioModel.ToComentarioDto());

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var commentModel = await _commentRepository.Delete(id);
            
            if (commentModel == null) return NotFound("Não existe este comentário");

            return NoContent();
        }
       
    }
}
