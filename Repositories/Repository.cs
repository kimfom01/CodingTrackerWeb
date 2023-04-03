using CodingTrackerWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodingTrackerWeb.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly CodingTrackerWebContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(CodingTrackerWebContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual void DeleteRecord(int id)
    {
        TEntity? entity = GetById(id);
        _dbSet.Remove(entity);
    }

    public virtual List<TEntity?> GetUserRecords(Expression<Func<TEntity?, bool>> predicate)
    {
        return _dbSet.Where(predicate).AsNoTracking().ToList();
    }

    public virtual TEntity? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public virtual void InsertRecord(TEntity? entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void UpdateRecord(int id, TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }
}
