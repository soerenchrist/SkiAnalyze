using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.SessionAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface ITracksService
{
    public Task<Track> AddTrack(Track track);
    public Task RemoveTrack(Track track);
    public Task<List<Track>> GetTracks(Guid sessionId);
    public Task ClearTracks(Guid sessionId);
}
