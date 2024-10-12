using GreatIdeas.Extensions.Paging;
using GreatIdeas.Repository.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GreatIdeas.Repository;

public abstract class RepositoryFactory<TContext, TEntity> :
    IDisposable, IRepositoryFactory<TContext, TEntity>
    where TContext : DbContext where TEntity : class
{
    protected readonly IDbContextFactory<TContext> DbContextFactory;

    protected TContext DbContext { get; set; } = default!;


    public RepositoryFactory(IDbContextFactory<TContext> dbContextFactory)
    {
        DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    public virtual async ValueTask<PagedList<TEntity>> GetPagedListAsync(
      PagingParams pagingParams,
      Expression<Func<TEntity, bool>>? predicate = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
      bool disableTracking = true,
      bool ignoreQueryFilters = false,
      CancellationToken cancellationToken = default)
    {
        DbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = DbContext.Set<TEntity>();

        IQueryable<TEntity> source = dbSet;
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

    public virtual async ValueTask<IEnumerable<TEntity>?> GetAllAsync(
      CancellationToken cancellationToken = default)
    {
        DbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = DbContext.Set<TEntity>();
        return await dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity>? GetAll(
      Expression<Func<TEntity, bool>>? predicate = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
      bool disableTracking = true,
      bool ignoreQueryFilters = false)
    {
        DbContext = DbContextFactory.CreateDbContext();
        var dbSet = DbContext.Set<TEntity>();

        IQueryable<TEntity> source = dbSet;
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

    public virtual ValueTask<TEntity?> FindAsync(params object[] keyValues)
    {
        DbContext = DbContextFactory.CreateDbContext();
        var dbSet = DbContext.Set<TEntity>();
        return dbSet.FindAsync(keyValues);
    }

    public virtual async ValueTask<TEntity?> FindAsync(
      object[] keyValues,
      CancellationToken cancellationToken)
    {
        DbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = DbContext.Set<TEntity>();
        return await dbSet.FindAsync(keyValues, cancellationToken);
    }

    public virtual async ValueTask<TEntity?> GetFirstOrDefaultAsync(
      Expression<Func<TEntity, bool>> predicate,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
      Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
      bool disableTracking = true,
      bool ignoreQueryFilters = false,
      CancellationToken cancellationToken = default)
    {
        DbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = DbContext.Set<TEntity>();

        IQueryable<TEntity> source = dbSet;
        if (disableTracking)
            source = source.AsNoTracking();

        if (include != null)
            source = include(source);

        if (ignoreQueryFilters)
            source = source.IgnoreQueryFilters();

        return orderBy != null ? await orderBy(source).FirstOrDefaultAsync(predicate, cancellationToken) :
          await source.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual TEntity? Insert(TEntity entity)
    {
        DbContext = DbContextFactory.CreateDbContext();
        var dbSet = DbContext.Set<TEntity>();
        return dbSet.Add(entity).Entity;
    }

    public virtual async ValueTask<EntityEntry<TEntity>?> InsertAsync(
      TEntity entity,
      CancellationToken cancellationToken = default)
    {
        DbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = DbContext.Set<TEntity>();
        return await dbSet.AddAsync(entity, cancellationToken);
    }

    public virtual void InsertRange(List<TEntity> entities)
    {
        DbContext = DbContextFactory.CreateDbContext();
        var dbSet = DbContext.Set<TEntity>();

        try
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities), "Items to insert must not be null");

            dbSet.AddRange(entities);
        }
        catch (Exception ex)
        {
            throw new Exception("Items could not be added: " + ex.Message);
        }
    }

    public virtual async ValueTask InsertRangeAsync(
      List<TEntity> entities,
      CancellationToken cancellationToken = default)
    {
        DbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = DbContext.Set<TEntity>();

        if (entities == null)
            throw new ArgumentNullException(nameof(entities), "Items to insert must not be null");
        try
        {
            await dbSet.AddRangeAsync(entities, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Items could not be added: " + ex.Message);
        }
    }

    public virtual void Update(TEntity entity)
    {
        DbContext = DbContextFactory.CreateDbContext();
        var dbSet = DbContext.Set<TEntity>();

        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "Item to update must not be null");
        try
        {
            dbSet.Update(entity);
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
        DbContext = DbContextFactory.CreateDbContext();
        var dbSet = DbContext.Set<TEntity>();

        if (entities.Count <= 0)
            throw new ArgumentNullException(nameof(entities), "Items to update must not be null");
        try
        {
            dbSet.UpdateRange(entities);
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

        DbContext = DbContextFactory.CreateDbContext();
        var dbSet = DbContext.Set<TEntity>();
        dbSet.Remove(entity);
    }

    public virtual void DeleteRange(List<TEntity> entities)
    {
        if (entities.Count <= 0)
            throw new ArgumentNullException(nameof(entities), "Items to delete must not be null");
        try
        {
            DbContext = DbContextFactory.CreateDbContext();
            var dbSet = DbContext.Set<TEntity>();
            dbSet.RemoveRange(entities);
        }
        catch (Exception ex)
        {
            throw new Exception("Items could not be deleted: " + ex.Message);
        }
    }

    public virtual async ValueTask<bool> ExistsAsync(
      Expression<Func<TEntity, bool>>? selector = null,
      CancellationToken cancellationToken = default)
    {
        DbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        var dbSet = DbContext.Set<TEntity>();
        return selector == null ? await dbSet.AnyAsync(cancellationToken) : await dbSet.AnyAsync(selector, cancellationToken);
    }

    public virtual async ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //DbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken);
        return await DbContext.SaveChangesAsync(cancellationToken);
    }

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