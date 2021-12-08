using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Core.Interfaces;

public interface ITrackFileParserStrategy
{
    public FileReadResult ReadFileContents(Stream fileContent);
}
