namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class RemoveRunRequest
{
    [FromRoute] public int TrackId { get; set; }
    [FromRoute] public int RunId { get; set; }
}