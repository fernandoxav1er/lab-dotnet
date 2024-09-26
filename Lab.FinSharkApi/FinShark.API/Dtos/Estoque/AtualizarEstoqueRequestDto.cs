using System.ComponentModel.DataAnnotations;

namespace FinShark.API.Dtos.Estoque
{
    public class AtualizarEstoqueRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Apelido só pode ter 10 caracteres")]
        public string Apelido { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "O nome da empresa só pode ter 10 caracteres")]
        public string NomeEmpresa { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal ValorCompra { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal Dividendo { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "O nome da Industria só pode ter 10 caracteres")]
        public string Industria { get; set; } = string.Empty;
        [Range(1, 5000000000)]
        public long Valuation { get; set; }
    }
}
