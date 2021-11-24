using SkiAnalyze.Core.PisteAggregate;

namespace SkiAnalyze.ApiModels;

public class PisteDto
{
    public string? Name { get; set; }
    public string? Reference { get; set; }
    public bool? Lit { get; set; }
    public bool? Snowmaking { get; set; }
    public PisteDifficulty? Difficulty { get; set; }
    public List<PisteNodeDto> Coordinates { get; set; } = new List<PisteNodeDto>();
}

public class PisteNodeDto
{
    public long OsmId { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
}
