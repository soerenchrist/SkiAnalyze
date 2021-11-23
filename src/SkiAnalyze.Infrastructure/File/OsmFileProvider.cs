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

    public void Dispose()
    {
        _fileStream?.Dispose();
    }

    public ValueTask DisposeAsync()
    {
         return _fileStream?.DisposeAsync() ?? new ValueTask();
    }

    public FileStream GetOsmFile()
    {
        _fileStream = System.IO.File.OpenRead(_filePath);
        return _fileStream;
    }
}
