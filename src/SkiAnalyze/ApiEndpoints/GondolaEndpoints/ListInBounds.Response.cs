using SkiAnalyze.ApiModels;

namespace SkiAnalyze.ApiEndpoints.GondolaEndpoints;

public class ListGondolasInBoundsResponse
{
    public ListGondolasInBoundsResponse(List<GondolaDto> gondolas)
    {
        Gondolas = gondolas;
        Count = gondolas.Count;
    }
    public List<GondolaDto> Gondolas { get; }
    public int Count { get; }
}
