namespace SkiAnalyze.ApiEndpoints.GondolaEndpoints;

public class GetGondolaRequest
{
    [FromRoute] public long Id { get; set; }
}
