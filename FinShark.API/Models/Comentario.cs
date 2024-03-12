namespace FinShark.API.Models
{
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
