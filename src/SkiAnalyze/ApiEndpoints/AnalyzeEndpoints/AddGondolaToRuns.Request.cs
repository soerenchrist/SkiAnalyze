namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class AddGondolaToRunsRequest
{
    public int TrackId { get; set; }
    public long GondolaId { get; set; }
    public int Position { get; set; }
}