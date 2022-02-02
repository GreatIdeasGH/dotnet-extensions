namespace GreatIdeas.Extensions.Paging;

public class PagingResponse<T> where T : class
{
    public List<T> Items { get; set; }

    public Metadata Metadata { get; set; }
}