using Catalogo.API.DTOs;
using Catalogo.API.Models;

namespace Catalogo.API.Extensions;

public static class CategoriaDTOMappingExtensions
{
    public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
    {
        if (categoria is null) return null;

        return new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemmUrl = categoria.ImagemmUrl
        };
    }

    public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null) return null;

        return new Categoria
        {
            ImagemmUrl = categoriaDTO.ImagemmUrl,
            Nome = categoriaDTO.Nome,
            CategoriaId = categoriaDTO.CategoriaId
        };
    }

    public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
    {
        if (categorias is null || !categorias.Any())
            return new List<CategoriaDTO>();

        return categorias.Select(categoria => new CategoriaDTO
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemmUrl = categoria.ImagemmUrl
        }).ToList();
    }
}