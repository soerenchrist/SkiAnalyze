using SkiAnalyze.Core.Common;

namespace SkiAnalyze.ApiModels;

public class AnalysisResultDto
{
    public Bounds Bounds { get; set; }
    public List<RunDto> Runs { get; set; } = new List<RunDto>();
}

public class RunDto
{
    public int Number { get; set; }
    public bool Downhill { get; set; }
    public GondolaDto? Gondola { get; set; }
    public List<TrackPoint> Coordinates { get; set; } = new();
    public double TotalDistance { get; set; }
    public double TotalElevation { get; set; }
    public double AverageSpeed { get; set; }
    public double MaxSpeed { get; set; }
    public string Color { get; set; } = string.Empty;
    public int TrackId { get; set; }
}