using System.Linq.Expressions;
using GreatIdeas.Extensions.Paging;
using GreatIdeas.Repository.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace GreatIdeas.Repository;

public interface IRepositoryFactory<TContext, TEntity, TDto> where TContext : DbContext where TEntity : class
{
    Task<IEnumerable<TDto>> GetAllProjectToCodeGenAsync(
        Expression<Func<TEntity, TDto>> selector,
        CancellationToken cancellationToken = default (CancellationToken));

    Task<IEnumerable<TDto>> GetAllProjectToAsync(
        CancellationToken cancellationToken = default (CancellationToken));

    Task<TDto> GetWithProjectToAsync(
        Expression<Func<TDto, bool>> predicate,
        CancellationToken cancellationToken = default (CancellationToken));

    Task<TDto> GetWithProjectToCodeGenAsync(
        Expression<Func<TDto, bool>> predicate,
        Expression<Func<TEntity, TDto>> mapper,
        CancellationToken cancellationToken = default (CancellationToken));

    PagedList<TDto> GetPagedDto(PagingParams pagingParams);

    Task<PagedList<TDto>> GetPagedDtoAsync(
        PagingParams pagingParams,
        CancellationToken cancellationToken = default (CancellationToken));

    Task<PagedList<TEntity>> GetPagedListAsync(
        PagingParams pagingParams,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        CancellationToken cancellationToken = default (CancellationToken),
        bool ignoreQueryFilters = false);

    Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default (CancellationToken));

    IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false);

    ValueTask<TEntity> FindAsync(params object[] keyValues);

    ValueTask<TEntity> FindAsync(
        object[] keyValues,
        CancellationToken cancellationToken);

    Task<TEntity> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default (CancellationToken));

    TEntity Insert(TEntity entity);

    ValueTask<EntityEntry<TEntity>> InsertAsync(
        TEntity entity,
        CancellationToken cancellationToken = default (CancellationToken));

    void InsertRange(List<TEntity> entities);

    Task InsertRangeAsync(
        List<TEntity> entities,
        CancellationToken cancellationToken = default (CancellationToken));

    void Update(TEntity entity);
    void UpdateDto(TEntity entity);
    void UpdateRange(List<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(List<TEntity> entities);

    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> selector = null,
        CancellationToken cancellationToken = default (CancellationToken));

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default (CancellationToken));
    void Dispose();
}