using SkiAnalyze.Core.Common;

namespace SkiAnalyze.ApiModels;

public class MapPreviewDto
{
    public Guid UserSessionId { get; set; }
    public int TrackId { get; set; }
    public Bounds? Bounds { get; set; }
    public List<Coordinate> Coordinates { get; set; } = new();
}
