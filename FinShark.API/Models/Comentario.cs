using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.API.Models
{
    [Table("Comentarios")]
    public class Comentario
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public int? EstoqueId { get; set; }
        public Estoque? Estoque { get; set; }
    }
}
