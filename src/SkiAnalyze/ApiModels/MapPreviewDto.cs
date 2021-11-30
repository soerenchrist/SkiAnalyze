namespace SkiAnalyze.ApiModels;

public class MapPreviewDto
{
    public int TrackId { get; set; }
    public Bounds? Bounds { get; set; }
    public List<Coordinate> Coordinates { get; set; } = new();
}
