namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class ListPaginatedRequest
{
    public float? NeLat { get; set; }
    public float? NeLon { get; set; }
    public float? SwLat { get; set; }
    public float? SwLon { get; set; }
    public string? SearchText { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
