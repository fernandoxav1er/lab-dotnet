using Catalogo.API.Context;
using Catalogo.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Catalogo.API.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _appDbContext;

    public Repository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _appDbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _appDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> Create(T entity)
    {
        _appDbContext.Set<T>().Add(entity);
        //await _appDbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<T> Update(T entity)
    {
        _appDbContext.Set<T>().Update(entity);
        //await _appDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Delete(T entity)
    {
        _appDbContext.Set<T>().Remove(entity);
        //await _appDbContext.SaveChangesAsync();
        return entity;
    }
}
