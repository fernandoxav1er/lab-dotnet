using System.ComponentModel.DataAnnotations;

namespace FinShark.API.Dtos.Comentario
{
    public class CriarComentarioDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "O título não pode ser menor que 5 caracteres")]
        [MaxLength(280, ErrorMessage ="O título não pode ser maior que 280 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "O conteúdo não pode ser menor que 5 caracteres")]
        [MaxLength(280, ErrorMessage = "O conteúdo não pode ser maior que 280 caracteres")]
        public string Conteudo { get; set; } = string.Empty;
    }
}
