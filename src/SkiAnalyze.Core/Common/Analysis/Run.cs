using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.PisteAggregate;

namespace SkiAnalyze.Core.Common.Analysis;

public class Run
{
    public int Id { get; set; }
    public int Number { get; set; }
    public bool Downhill { get; set; }
    public Gondola? Gondola { get; set; }
    public Piste? Piste { get; set; }
    public List<TrackPoint> Coordinates { get; set; } = new();
}