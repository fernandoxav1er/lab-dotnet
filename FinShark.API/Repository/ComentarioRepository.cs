using FinShark.API.Data;
using FinShark.API.Interfaces;
using FinShark.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.API.Repository
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly ApplicationDBContext _context;
        public ComentarioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comentario?> CreateAsync(Comentario comentarioModel)
        {
            await _context.Comentarios.AddAsync(comentarioModel);
            await _context.SaveChangesAsync();
            return comentarioModel;
        }

        public async Task<Comentario?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comentarios.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
                return null;
            
            _context.Comentarios.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comentario>> GetAllAsync()
        {
            return await _context.Comentarios.ToListAsync();
        }
        public async Task<Comentario?> GetByIdAsync(int id)
        {
            return await _context.Comentarios.FindAsync(id);
        }
    }
}
