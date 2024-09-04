using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Catalogo.API.Models;

[Table("Produtos")]
public class Produto : IValidatableObject
{
    [JsonIgnore]
    public Categoria? Categoria { get; set; }
    public int CategoriaId { get; set; }
    public DateTime DataCadastro { get; set; }
    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }
    public float Estoque { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres", MinimumLength = 5)]
    public string? Nome { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }
    [Key]
    public int ProdutoId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return
                    new ValidationResult("A primeira letra do produto deve ser maiúscula!",
                        new[] { nameof(this.Nome) }
                    );
            }
        }
    }
}