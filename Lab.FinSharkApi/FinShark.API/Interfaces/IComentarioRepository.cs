using FinShark.API.Models;

namespace FinShark.API.Interfaces
{
    public interface IComentarioRepository
    {
        Task<List<Comentario>> GetAll();
        Task<Comentario?> GetById(int id);
        Task<Comentario?> Create(Comentario comentarioModel);
        Task<Comentario?> Delete(int id);
    }
}
