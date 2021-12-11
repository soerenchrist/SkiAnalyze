namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class GetGondolasRequest
{
    [FromRoute] public long SkiAreaId { get; set; }
}
