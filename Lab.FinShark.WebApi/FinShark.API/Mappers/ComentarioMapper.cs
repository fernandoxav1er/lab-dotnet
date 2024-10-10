using FinShark.API.Dtos.Comentario;
using FinShark.API.Models;
using System.Runtime.CompilerServices;

namespace FinShark.API.Mappers
{
    public static class ComentarioMapper
    {
        public static ComentarioDto ToComentarioDto(this Comentario comentarioModel)
        {
            return new ComentarioDto
            {
                Id = comentarioModel.Id,
                Titulo = comentarioModel.Titulo,
                Conteudo = comentarioModel.Conteudo,
                CriadoEm = comentarioModel.CriadoEm,
                EstoqueId = comentarioModel.EstoqueId
            };
        }
        public static Comentario ToComentarioCreateDto(this CriarComentarioDto comentDto, int estoqueid)
        {
            return new Comentario
            {
                Titulo = comentDto.Titulo,
                Conteudo = comentDto.Conteudo,
                EstoqueId = estoqueid
            };
        }
    }
}
