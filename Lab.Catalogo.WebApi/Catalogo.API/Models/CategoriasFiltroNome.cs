namespace Catalogo.API.Models;

public class CategoriasFiltroNome : QueryStringParameters
{
    public string? Nome { get; set; }
}