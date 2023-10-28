using System.Linq.Expressions;

namespace FogTalk.Domain.Repositories;

public interface IGenericRepository<TEntity, in TKey> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id);
    public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>? include = null);
    Task<int> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity?>  GetNextRecordAsync(Expression<Func<TEntity, bool>>filter);
    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
}