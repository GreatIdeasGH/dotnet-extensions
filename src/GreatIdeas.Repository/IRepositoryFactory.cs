using GreatIdeas.Extensions.Paging;
using GreatIdeas.Repository.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreatIdeas.Repository;

public interface IRepositoryFactory<TContext, TEntity, TDto> : IRepositoryFactory<TContext, TEntity>
    where TContext : DbContext
    where TEntity : class
{
    ValueTask<IEnumerable<TDto>?> GetAllProjectToCodeGenAsync(
        Expression<Func<TEntity, TDto>> selector,
        CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<TDto>?> GetAllProjectToAsync(
        CancellationToken cancellationToken = default);

    ValueTask<TDto?> GetWithProjectToAsync(
        Expression<Func<TDto, bool>> predicate,
        CancellationToken cancellationToken = default);

    ValueTask<TDto?> GetWithProjectToCodeGenAsync(
        Expression<Func<TDto, bool>> predicate,
        Expression<Func<TEntity, TDto>> mapper,
        CancellationToken cancellationToken = default);

    PagedList<TDto> GetPagedDto(PagingParams pagingParams);

    ValueTask<PagedList<TDto>> GetPagedDtoAsync(
        PagingParams pagingParams,
        CancellationToken cancellationToken = default);

}