using FinShark.API.Dtos.Estoque;
using FinShark.API.Models;

namespace FinShark.API.Interfaces
{
    public interface IEstoqueRepository
    {
        Task<List<Estoque>> GetAllAsync();
        Task<Estoque?> GetByIdAsync(int id);
        Task<Estoque> CreateAsync(Estoque estoqueModel);
        Task<Estoque?> UpdateAsync(int id, AtualizarEstoqueRequestDto estoqueDto);
        Task<Estoque?> DeleteAsync(int id);
        Task<bool> EstoqueExist(int id);
    }
}
