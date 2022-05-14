using GreatIdeas.Extensions.Paging;
using GreatIdeas.Repository.Paging;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreatIdeas.Repository;

public class RepositoryFactory<TContext, TEntity, TDto> :
    RepositoryFactory<TContext, TEntity>, IRepositoryFactory<TContext, TEntity, TDto>
  where TContext : DbContext where TEntity : class
{
    public RepositoryFactory(IDbContextFactory<TContext> dbContextFactory)
      : base(dbContextFactory)
    {
    }

    public virtual async Task<IEnumerable<TDto>> GetAllProjectToCodeGenAsync(
      Expression<Func<TEntity, TDto>> selector,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        RepositoryFactory<TContext, TEntity, TDto> repositoryFactory = this;
        List<TDto> projectToCodeGenAsync = new List<TDto>();

        if (selector != null)
            projectToCodeGenAsync = await repositoryFactory.DbContextFactory.CreateDbContext().Set<TEntity>()
              .Select(selector).ToListAsync(cancellationToken);

        return projectToCodeGenAsync;
    }

    public virtual async Task<IEnumerable<TDto>> GetAllProjectToAsync(
      CancellationToken cancellationToken = default(CancellationToken))
    {
        var _dbset = this.DbContextFactory.CreateDbContext().Set<TEntity>();
        return await _dbset.AsNoTracking().ProjectToType<TDto>().ToListAsync(cancellationToken);
    }

    public virtual async Task<TDto> GetWithProjectToAsync(
      Expression<Func<TDto, bool>> predicate,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        var _dbset = this.DbContextFactory.CreateDbContext().Set<TEntity>();
        return await _dbset.AsNoTracking().ProjectToType<TDto>()
          .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<TDto> GetWithProjectToCodeGenAsync(
      Expression<Func<TDto, bool>> predicate,
      Expression<Func<TEntity, TDto>> mapper,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        var _dbset = DbContextFactory.CreateDbContext().Set<TEntity>();
        return await _dbset.AsNoTracking().Select(mapper).FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual PagedList<TDto> GetPagedDto(PagingParams pagingParams)
    {
        var _dbset = DbContextFactory.CreateDbContext().Set<TEntity>();
        return PagedList<TDto>.ToPagedList(_dbset.AsNoTracking()
        .AsQueryable()
        .ProjectToType<TDto>(), pagingParams.PageIndex, pagingParams.PageSize);
    }


    public virtual Task<PagedList<TDto>> GetPagedDtoAsync(
      PagingParams pagingParams,
      CancellationToken cancellationToken = default(CancellationToken))
    {
        var _dbset = DbContextFactory.CreateDbContext().Set<TEntity>();
        return PagedList<TDto>
          .ToPagedListAsync(_dbset.AsNoTracking()
          .AsQueryable()
          .ProjectToType<TDto>(), pagingParams.PageIndex, pagingParams.PageSize, cancellationToken);
    }
}