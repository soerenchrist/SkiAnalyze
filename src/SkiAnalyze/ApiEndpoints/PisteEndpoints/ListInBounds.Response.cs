using SkiAnalyze.ApiModels;

namespace SkiAnalyze.ApiEndpoints.PisteEndpoints;

public class ListPistesInBoundsResponse
{
    public ListPistesInBoundsResponse(List<PisteDto> pistes)
    {
        Pistes = pistes;
        Count = pistes.Count;
    }
    public List<PisteDto> Pistes { get; }
    public int Count { get; }
}
