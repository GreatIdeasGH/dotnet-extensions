using GreatIdeas.Extensions.Paging;
using GreatIdeas.Repository.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GreatIdeas.Repository;

public interface IRepositoryFactory<TContext, TEntity> where TContext : DbContext where TEntity : class
{
    void Delete(TEntity entity);
    void DeleteRange(List<TEntity> entities);
    void Dispose();
    ValueTask<bool> ExistsAsync(Expression<Func<TEntity, bool>>? selector = null, CancellationToken cancellationToken = default);
    ValueTask<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken);
    ValueTask<TEntity?> FindAsync(params object[] keyValues);
    IQueryable<TEntity>? GetAll(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true, bool ignoreQueryFilters = false);
    ValueTask<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default);
    ValueTask<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
    ValueTask<PagedList<TEntity>> GetPagedListAsync(PagingParams pagingParams, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
    TEntity? Insert(TEntity entity);
    ValueTask<EntityEntry<TEntity>?> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    void InsertRange(List<TEntity> entities);
    ValueTask InsertRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
    ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void UpdateDto(TEntity entity);
    void UpdateRange(List<TEntity> entities);
}