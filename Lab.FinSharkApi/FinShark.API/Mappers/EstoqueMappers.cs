using FinShark.API.Dtos.Estoque;
using FinShark.API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace FinShark.API.Mappers
{
    public static class EstoqueMappers
    {
        public static EstoqueDto ToEstoqueDto(this Estoque estoqueModel)
        {
            return new EstoqueDto
            {
                Id = estoqueModel.Id,
                Apelido = estoqueModel.Apelido,
                NomeEmpresa = estoqueModel.NomeEmpresa,
                ValorCompra = estoqueModel.ValorCompra,
                Dividendo = estoqueModel.Dividendo,
                Industria = estoqueModel.Industria,
                Valuation = estoqueModel.Valuation,
                Comentarios = estoqueModel.Comentarios.Select(c => c.ToComentarioDto()).ToList()
            };
        }

        public static Estoque ToEstoqueCreateDto(this CriarEstoqueRequestDto estoqueDto)
        {
            return new Estoque
            {
                Apelido = estoqueDto.Apelido,
                NomeEmpresa = estoqueDto.NomeEmpresa,
                ValorCompra = estoqueDto.ValorCompra,
                Dividendo = estoqueDto.Dividendo,
                Industria = estoqueDto.Industria,
                Valuation = estoqueDto.Valuation
            };
        }
    }
}
