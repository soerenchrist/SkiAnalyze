using SkiAnalyze.Core.Entities.TrackAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface ITrackFileParserStrategy
{
    public List<Run> ReadFileContents(Stream fileContent);
}
