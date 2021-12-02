namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class ListInBoundsRequest
{
    public float NeLat { get; set; }
    public float NeLon { get; set; }
    public float SwLat { get; set; }
    public float SwLon { get; set; }
}
