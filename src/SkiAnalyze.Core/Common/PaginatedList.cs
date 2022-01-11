namespace SkiAnalyze.Core.Common;

public class PaginatedList<T>
{
    public List<T> Data { get; set; }
    public int Count { get; set; }
    public int Page { get; set; }

    public PaginatedList(List<T> data, int count, int page)
    {
        Data = data;
        Count = count;
        Page = page;
    }
}