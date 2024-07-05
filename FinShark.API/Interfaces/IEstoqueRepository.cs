using FinShark.API.Dtos.Estoque;
using FinShark.API.Helpers;
using FinShark.API.Models;

namespace FinShark.API.Interfaces
{
    public interface IEstoqueRepository
    {
        Task<List<Estoque>> GetAll(QueryObject query);
        Task<List<Estoque>> GetPagination(QueryPagination query);
        Task<Estoque?> GetById(int id);
        Task<Estoque?> GetBySymbol(string symbol);
        Task<Estoque> Create(Estoque estoqueModel);
        Task<Estoque?> Update(int id, AtualizarEstoqueRequestDto estoqueDto);
        Task<Estoque?> Delete(int id);
        Task<bool> EstoqueExist(int id);
    }
}
