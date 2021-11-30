namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class PreviewRequest
{
    [FromRoute] public int TrackId { get; set; }
}
