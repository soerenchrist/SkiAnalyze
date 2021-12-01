namespace SkiAnalyze.Blazor.Services.Interfaces;

public interface ITracksService
{
    Task<List<TrackDto>> GetTracks();
}
