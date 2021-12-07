namespace SkiAnalyze.ApiModels;

public class SkiAreaDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double CenterLatitude { get; set; }
    public double CenterLongitude { get; set; }
}

public class SkiAreaDetailDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? Wikidata { get; set; }
    public string? AlternativeName { get; set; }
    public double CenterLatitude { get; set; }
    public double CenterLongitude { get; set; }
    public List<SkiAreaNodeDto> Nodes { get; set; } = new();
}


public class SkiAreaNodeDto
{
    public long OsmId { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
}
