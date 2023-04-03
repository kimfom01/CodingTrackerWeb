using CodingTrackerWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodingTrackerWeb.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly CodingTrackerWebContext _dbContext;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(CodingTrackerWebContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public virtual void DeleteRecord(int id)
    {
        TEntity? entity = GetById(id);
        DbSet.Remove(entity);
    }

    public virtual List<TEntity?> GetUserRecords(Expression<Func<TEntity?, bool>> predicate)
    {
        return DbSet.Where(predicate).AsNoTracking().ToList();
    }

    public virtual TEntity? GetById(int id)
    {
        return DbSet.Find(id);
    }

    public virtual void InsertRecord(TEntity? entity)
    {
        DbSet.Add(entity);
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
