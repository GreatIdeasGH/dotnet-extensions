using GreatIdeas.Extensions.Paging;
using GreatIdeas.Repository.Paging;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GreatIdeas.Repository
{
    public interface IRepositoryFactory<TContext, TEntity> where TContext:DbContext where TEntity : class
    {
        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entities);
        void Dispose();
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null, CancellationToken cancellationToken = default);
        ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);
        ValueTask<TEntity> FindAsync(params object[] keyValues);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
        Task<PagedList<TEntity>> GetPagedListAsync(PagingParams pagingParams, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true, CancellationToken cancellationToken = default, bool ignoreQueryFilters = false);
        TEntity Insert(TEntity entity);
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        void InsertRange(List<TEntity> entities);
        Task InsertRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void UpdateDto(TEntity entity);
        void UpdateRange(List<TEntity> entities);
    }
}