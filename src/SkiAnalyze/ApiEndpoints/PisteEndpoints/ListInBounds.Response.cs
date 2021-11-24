using SkiAnalyze.ApiModels;

namespace SkiAnalyze.ApiEndpoints.PisteEndpoints;

public class ListPistesInBoundsResponse
{
    public List<PisteDto> Pistes { get; set; } = new();
}
