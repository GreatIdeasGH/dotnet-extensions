using System.Linq.Expressions;
using GreatIdeas.Extensions.Paging;
using GreatIdeas.Repository.Paging;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GreatIdeas.Repository;

public class RepositoryFactory<TContext, TEntity, TDto>
    : RepositoryFactory<TContext, TEntity>,
        IRepositoryFactory<TContext, TEntity, TDto>
    where TContext : DbContext
    where TEntity : class
{
    public RepositoryFactory(IDbContextFactory<TContext> dbContextFactory)
        : base(dbContextFactory) { }

    public virtual async ValueTask<IEnumerable<TDto>?> GetAllProjectToCodeGenAsync(
        Expression<Func<TEntity, TDto>>? selector,
        CancellationToken cancellationToken = default
    )
    {
        RepositoryFactory<TContext, TEntity, TDto> repositoryFactory = this;
        List<TDto> projectToCodeGenAsync = [];

        if (selector is not null)
            projectToCodeGenAsync = await (
                await repositoryFactory.DbContextFactory.CreateDbContextAsync(cancellationToken)
            )
                .Set<TEntity>()
                .Select(selector)
                .ToListAsync(cancellationToken);

        return projectToCodeGenAsync;
    }

    public virtual async ValueTask<IEnumerable<TDto>?> GetAllProjectToAsync(CancellationToken cancellationToken = default)
    {
        var dbset = (
            await DbContextFactory.CreateDbContextAsync(cancellationToken)
        ).Set<TEntity>();
        return await dbset.AsNoTracking().ProjectToType<TDto>().ToListAsync(cancellationToken);
    }

    public virtual async ValueTask<TDto?> GetWithProjectToAsync(
        Expression<Func<TDto, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        var dbset = (
            await DbContextFactory.CreateDbContextAsync(cancellationToken)
        ).Set<TEntity>();
        return await dbset
            .AsNoTracking()
            .ProjectToType<TDto>()
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async ValueTask<TDto?> GetWithProjectToCodeGenAsync(
        Expression<Func<TDto, bool>> predicate,
        Expression<Func<TEntity, TDto>> mapper,
        CancellationToken cancellationToken = default
    )
    {
        var dbset = (await DbContextFactory.CreateDbContextAsync(cancellationToken)).Set<TEntity>();
        return await dbset
            .AsNoTracking()
            .Select(mapper)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual PagedList<TDto> GetPagedDto(PagingParams pagingParams)
    {
        var dbset = DbContextFactory.CreateDbContext().Set<TEntity>();
        return PagedList<TDto>.ToPagedList(
            dbset.AsNoTracking().AsQueryable().ProjectToType<TDto>(),
            pagingParams.PageIndex,
            pagingParams.PageSize
        );
    }

    public virtual async ValueTask<PagedList<TDto>> GetPagedDtoAsync(
        PagingParams pagingParams,
        CancellationToken cancellationToken = default
    )
    {
        var dbset = (await DbContextFactory.CreateDbContextAsync(cancellationToken)).Set<TEntity>();
        return await PagedList<TDto>.ToPagedListAsync(
            dbset.AsNoTracking().AsQueryable().ProjectToType<TDto>(),
            pagingParams.PageIndex,
            pagingParams.PageSize,
            cancellationToken
        );
    }
}
