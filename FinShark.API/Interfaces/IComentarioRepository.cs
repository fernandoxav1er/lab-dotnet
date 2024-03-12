using FinShark.API.Models;

namespace FinShark.API.Interfaces
{
    public interface IComentarioRepository
    {
        Task<List<Comentario>> GetAllAsync();
        Task<Comentario?> GetByIdAsync(int id);
        Task<Comentario?> CreateAsync(Comentario comentarioModel);
        Task<Comentario?> DeleteAsync(int id);
    }
}
