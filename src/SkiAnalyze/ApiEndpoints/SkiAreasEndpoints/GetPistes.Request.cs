namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class GetPistesRequest
{
    [FromRoute] public long SkiAreaId { get; set; }
}
