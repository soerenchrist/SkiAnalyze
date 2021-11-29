using SkiAnalyze.Core.SessionAggregate.Events;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.SessionAggregate;

public class UserSession : BaseEntity<Guid>
{
    public long LastUsage { get; set; }

    private List<Track> _tracks = new List<Track>();
    public IEnumerable<Track> Tracks => _tracks.AsReadOnly();

    public void AddTrack(Track track)
    {
        _tracks.Add(track);
        
        var addedEvent = new TrackAddedEvent(this, track);
        Events.Add(addedEvent);
    }

    public void ClearTracks()
    {
        _tracks.Clear();
    }

    public void RemoveTrack(Track track)
    {
        _tracks.Remove(track);
    }
}
