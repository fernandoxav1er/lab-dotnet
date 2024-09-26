using System.Linq.Expressions;

namespace Catalogo.API.Interfaces.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> Create (T entity);
    Task<T> Update (T entity);
    Task<T> Delete(T entity);
}
