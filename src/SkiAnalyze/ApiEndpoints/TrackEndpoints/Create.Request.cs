namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class CreateTrackRequest
{
    public IFormFile File { get; set; } = default!;
    public string Color { get; set; } = string.Empty;
    public TrackFileType FileType { get; set; }
}
