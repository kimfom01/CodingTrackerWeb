using System.Linq.Expressions;

namespace CodingTrackerWeb.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public void InsertRecord(TEntity codingHour);
    public void DeleteRecord(int id);
    public IEnumerable<TEntity> GetUserRecords(Expression<Func<TEntity, bool>> predicate);
    public void UpdateRecord(int id, TEntity codingHour);
    public TEntity? GetById(int id);
}
