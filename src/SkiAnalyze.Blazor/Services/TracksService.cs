
using System.Net.Http.Json;

namespace SkiAnalyze.Blazor.Services;

public class TracksService : ITracksService
{
    private readonly HttpClient _client;

    public TracksService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<TrackDto>> GetTracks()
    {
        var results = await _client.GetFromJsonAsync<List<TrackDto>>("/api/tracks");
        return results ?? new List<TrackDto>();
    }
}
