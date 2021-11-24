using SkiAnalyze.ApiModels;

namespace SkiAnalyze.ApiEndpoints.GondolaEndpoints;

public class ListInBoundsResponse
{
    public List<GondolaDto> Gondolas { get; set; } = new();
}
