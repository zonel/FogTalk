using System.Linq.Expressions;
using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FogTalk.Infrastructure.Repository;

internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
{
    #region ctor and props
    
    private readonly FogTalkDbContext _dbContext;
    private readonly DbSet<TEntity> _entities;

    public GenericRepository(FogTalkDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<TEntity>();
    }
    
    #endregion

    //GetByIdAsync
    public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

    //GetAllAsync
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();

        if (include != null) 
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }
    
    //AddAsync
    public async Task<int> AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        var property = _dbContext.Entry(entity).Property("Id");
        return (int)(property.CurrentValue ?? throw new InvalidOperationException());
    }   

    //UpdateAsync
    public async Task UpdateAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    //DeleteAsync
    public async Task DeleteAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
    
    //GetNextRecordAsync
    public async Task<TEntity?> GetNextRecordAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _entities.Where(filter).FirstOrDefaultAsync();
    }
    
    //AnyAsync
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(predicate);
    }
}