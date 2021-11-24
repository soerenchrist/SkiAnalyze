namespace SkiAnalyze.ApiEndpoints.PisteEndpoints;

public class ListPistesInBoundsRequest
{
    public float NeLat { get; set; }
    public float NeLon { get; set; }
    public float SwLat { get; set; }
    public float SwLon { get; set; }
}
