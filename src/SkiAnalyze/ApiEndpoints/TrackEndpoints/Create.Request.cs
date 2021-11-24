namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class CreateTrackRequest
{
    public Guid? UserSessionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string GpxFileContent { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
