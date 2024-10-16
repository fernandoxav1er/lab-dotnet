using System.ComponentModel.DataAnnotations;

namespace Catalogo.API.Models;

public class CategoriaDTO
{
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemmUrl { get; set; }
}
