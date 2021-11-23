namespace SkiAnalyze.Core.Interfaces;

public interface IOsmFileProvider : IDisposable, IAsyncDisposable
{
    FileStream GetOsmFile();
}
