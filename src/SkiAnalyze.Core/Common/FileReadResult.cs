using SkiAnalyze.Core.Entities.TrackAggregate;

namespace SkiAnalyze.Core.Common;

public class FileReadResult
{
    public List<Run> Runs { get; }
    public string TrackName { get; }

    public FileReadResult(string trackName, List<Run> runs)
    {
        TrackName = trackName;
        Runs = runs;
    }
}
