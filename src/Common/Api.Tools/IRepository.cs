namespace Api.Tools;

public interface IRepository<T, in TKey>
    where T : class
    where TKey : IEquatable<TKey>
{
    Task<T> GetByIdAsync(TKey id);
    Task<T> FindAsync(TKey id);
    T AddOrUpdate(T entity);
    void Delete(TKey id);
    Task SaveChangesAsync();
}

public interface IRepository<T> : IRepository<T, int>
    where T : class
{        
}