using SkiAnalyze.ApiModels;

namespace SkiAnalyze.ApiEndpoints.GondolaEndpoints;

public class ListGondolasInBoundsResponse
{
    public List<GondolaDto> Gondolas { get; set; } = new();
}
