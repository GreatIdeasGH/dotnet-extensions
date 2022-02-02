using System.Linq.Expressions;
using GreatIdeas.Extensions.Paging;
using GreatIdeas.Repository.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace GreatIdeas.Repository;

public abstract class RepositoryFactory<TContext, TEntity> :
    IDisposable, IRepositoryFactory<TContext, TEntity> 
    where TContext : DbContext where TEntity : class
{
    private readonly IDbContextFactory<TContext> _dbContextFactory;

    protected DbSet<TEntity> DbSet { get; set; }

    protected TContext DbContext { get; set; }

    public RepositoryFactory(IDbContextFactory<TContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        DbContext = _dbContextFactory.CreateDbContext();
        DbSet = DbContext.Set<TEntity>();
    }

    public virtual async Task<PagedList<TEntity>> GetPagedListAsync(
      PagingParams pagingParams,
      Expression<Func<TEntity, bool>> predicate = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
      bool disableTracking = true,
      CancellationToken cancellationToken = default(CancellationToken),
      bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> source = DbSet;
        if (disableTracking)
            source = source.AsNoTracking();

        if (include != null)
            source = include(source);

        if (predicate != null)
            source = source.Where(predicate);

        if (ignoreQueryFilters)
            source = source.IgnoreQueryFilters();

        return orderBy != null ? await PagedList<TEntity>.
          ToPagedListAsync(orderBy(source), pagingParams.PageIndex, pagingParams.PageSize, cancellationToken) :
          await PagedList<TEntity>.ToPagedListAsync(source, pagingParams.PageIndex, pagingParams.PageSize, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> GetAll(
      Expression<Func<TEntity, bool>> predicate = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
      bool disableTracking = true,
      bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> source = DbSet;
        if (disableTracking)
            source = source.AsNoTracking();
        if (include != null)
            source = include(source);
        if (predicate != null)
            source = source.Where(predicate);
        if (ignoreQueryFilters)
            source = source.IgnoreQueryFilters();
        return orderBy != null ? orderBy(source) : source;
    }

    public virtual ValueTask<TEntity> FindAsync(params object[] keyValues) => DbSet.FindAsync(keyValues);

    public virtual ValueTask<TEntity> FindAsync(
      object[] keyValues,
      CancellationToken cancellationToken)
    {
        return DbSet.FindAsync(keyValues, cancellationToken);
    }

    public virtual async Task<TEntity> GetFirstOrDefaultAsync(
      Expression<Func<TEntity, bool>> predicate,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
      bool disableTracking = true,
      bool ignoreQueryFilters = false,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        IQueryable<TEntity> source = DbSet;
        if (disableTracking)
            source = source.AsNoTracking();

        if (include != null)
            source = include(source);

        if (ignoreQueryFilters)
            source = source.IgnoreQueryFilters();

        return orderBy != null ? await orderBy(source).FirstOrDefaultAsync(predicate, cancellationToken) :
          await source.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual TEntity Insert(TEntity entity) => DbSet.Add(entity).Entity;

    public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(
      TEntity entity,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return DbSet.AddAsync(entity, cancellationToken);
    }

    public virtual void InsertRange(List<TEntity> entities)
    {
        try
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities), "Items to insert must not be null");

            DbSet.AddRange(entities);
        }
        catch (Exception ex)
        {
            throw new Exception("Items could not be added: " + ex.Message);
        }
    }

    public virtual async Task InsertRangeAsync(
      List<TEntity> entities,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities), "Items to insert must not be null");
        try
        {
            await DbSet.AddRangeAsync(entities, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Items could not be added: " + ex.Message);
        }
    }

    public virtual void Update(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Item to update must not be null");
        try
        {
            DbSet.Update(entity);
        }
        catch (Exception ex)
        {
            throw new Exception("entity could not be updated: " + ex.Message);
        }
    }

    public void UpdateDto(TEntity entity)
    {
    }

    public virtual void UpdateRange(List<TEntity> entities)
    {
        if (entities.Count <= 0)
            throw new ArgumentNullException(nameof(entities), "Items to update must not be null");
        try
        {
            DbSet.UpdateRange(entities);
        }
        catch (Exception ex)
        {
            throw new Exception("Items could not be updated: " + ex.Message);
        }
    }

    public virtual void Delete(TEntity entity)
    {
        if (entity == null)
            return;
        DbSet.Remove(entity);
    }

    public virtual void DeleteRange(List<TEntity> entities)
    {
        if (entities.Count <= 0)
            throw new ArgumentNullException(nameof(entities), "Items to delete must not be null");
        try
        {
            DbSet.RemoveRange(entities);
        }
        catch (Exception ex)
        {
            throw new Exception("Items could not be deleted: " + ex.Message);
        }
    }

    public virtual async Task<bool> ExistsAsync(
      Expression<Func<TEntity, bool>> selector = null,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        return selector == null ? await DbSet.AnyAsync(cancellationToken) : await DbSet.AnyAsync(selector, cancellationToken);
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) => await DbContext.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing)
            return;
        DbContext?.Dispose();
    }
}