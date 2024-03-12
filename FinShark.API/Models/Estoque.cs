using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.API.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        public string Apelido { get; set; } = string.Empty;
        public string NomeEmpresa { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorCompra { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Dividendo { get; set; }
        public string Industria { get; set; } = string.Empty;
        public long Valuation { get; set; }
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
