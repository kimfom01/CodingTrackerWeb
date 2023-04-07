using CodingTrackerWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodingTrackerWeb.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly CodingTrackerWebContext DbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(CodingTrackerWebContext dbContext)
    {
        DbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual void DeleteRecord(int id)
    {
        var entity = GetById(id);
        
        if (entity is not null)
        {
            _dbSet.Remove(entity);
        }
    }

    public virtual IEnumerable<TEntity> GetUserRecords(Expression<Func<TEntity, bool>> predicate)
    {
        var records = _dbSet.Where(predicate).AsNoTracking();

        return records.ToList();
    }

    public virtual TEntity? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public virtual void InsertRecord(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void UpdateRecord(int id, TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
    }
}
