using ControleContatos.MVC.Data.Map;
using ControleContatos.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos.MVC.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) {}
        public DbSet<ContatoModel> Contato { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
