namespace GreatIdeas.Extensions.Paging;

public class PagingParams
{
    private const int MaxPageSize = 100;
    private int _pageIndex = 1;
    private int _pageSize = 10;

    public string? OrderBy { get; set; }

    public string? Search { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value >= 1 ? value : throw new Exception("Bad request for page number!");
    }

    public virtual int PageSize
    {
        get => _pageSize;
        set
        {
            if (value < 1)
                throw new Exception("Bad request for page size!");
            if (value > MaxPageSize)
                value = MaxPageSize;
            _pageSize = value;
        }
    }
}