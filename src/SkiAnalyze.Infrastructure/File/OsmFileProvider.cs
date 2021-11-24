using SkiAnalyze.Core.Interfaces;

namespace SkiAnalyze.Infrastructure.File;

internal class OsmFileProvider : IOsmFileProvider
{
    private readonly string _filePath;
    private FileStream? _fileStream;
    public OsmFileProvider(string filePath)
    {
        _filePath = filePath;
    }

    public FileStream GetOsmFile()
    {
        _fileStream = System.IO.File.OpenRead(_filePath);
        return _fileStream;
    }
}
