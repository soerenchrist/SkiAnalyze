using SkiAnalyze.Core.Common;

namespace SkiAnalyze.ApiModels;

public class AnalysisResultDto
{
    public bool IsRunning { get; set; }
    public Bounds Bounds { get; set; }
    public List<RunDto> Runs { get; set; } = new List<RunDto>();
}

public class RunDto
{
    public int Number { get; set; }
    public bool Downhill { get; set; }
    public GondolaDto? Gondola { get; set; }
    public PisteDto? Piste { get; set; }
    public List<TrackPoint> Coordinates { get; set; } = new();

}
