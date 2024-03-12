using FinShark.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            
        }

        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

    }
}
