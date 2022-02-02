using GreatIdeas.Extensions.Paging;
using Microsoft.EntityFrameworkCore;

namespace GreatIdeas.Repository.Paging;

public class PagedList<T> : List<T>
{
    public Metadata Metadata { get; set; }

    public PagedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        Metadata = new Metadata
        {
            TotalCount = count,
            PageSize = pageSize,
            PageIndex = pageIndex,
            TotalPages = (int) Math.Ceiling(count / (double) pageSize)
        };
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(
        IQueryable<T> source,
        int pageIndex,
        int pageSize)
    {
        int count = source.Count();
        return new PagedList<T>(source.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList(), count, pageIndex, pageSize);
    }

    public static async Task<PagedList<T>> ToPagedListAsync(
        IQueryable<T> source,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        int count = await source.CountAsync(cancellationToken);
        return new PagedList<T>(await source.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken), count, pageIndex, pageSize);
    }
}