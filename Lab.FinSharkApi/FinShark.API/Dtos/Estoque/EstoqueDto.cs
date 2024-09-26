using FinShark.API.Dtos.Comentario;

namespace FinShark.API.Dtos.Estoque
{
    public class EstoqueDto
    {
        public int Id { get; set; }
        public string Apelido { get; set; } = string.Empty;
        public string NomeEmpresa { get; set; } = string.Empty;
        public decimal ValorCompra { get; set; }
        public decimal Dividendo { get; set; }
        public string Industria { get; set; } = string.Empty;
        public long Valuation { get; set; }
        public List<ComentarioDto> Comentarios { get; set; }
    }
}
