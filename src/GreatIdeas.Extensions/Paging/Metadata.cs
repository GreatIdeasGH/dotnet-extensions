namespace GreatIdeas.Extensions.Paging;

public class Metadata
{
    public int TotalCount { get; set; }

    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    public int TotalPages { get; set; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;
}