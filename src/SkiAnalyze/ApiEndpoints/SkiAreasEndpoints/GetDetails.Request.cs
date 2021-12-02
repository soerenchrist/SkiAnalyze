namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class GetDetailsRequest
{
    [FromRoute] public long SkiAreaId { get; set; }
}
