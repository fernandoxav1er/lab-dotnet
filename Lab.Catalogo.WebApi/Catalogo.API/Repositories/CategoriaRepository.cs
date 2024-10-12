using Catalogo.API.Context;
using Catalogo.API.Interfaces;
using Catalogo.API.Models;

namespace Catalogo.API.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

}
