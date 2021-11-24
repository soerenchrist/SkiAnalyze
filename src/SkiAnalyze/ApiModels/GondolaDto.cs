using SkiAnalyze.Core.GondolaAggregate;

namespace SkiAnalyze.ApiModels;

public class GondolaDto
{
    public string? Name { get; set; }
    public string Type { get; set; } = string.Empty;
    public int? Occupancy { get; set; }
    public int? Capacity { get; set; }
    public string? Reference { get; set; }
    public bool? OneWay { get; set; }
    public bool? Bubble { get; set; }
    public bool? Heating { get; set; }
    public string? Wikidata { get; set; }
    public string? Wikipedia { get; set; }
    public string? Description { get; set; }
    public double? Duration { get; set; }
    public List<GondolaCoordinateDto> Coordinates { get; set; } = new();
}

public class GondolaCoordinateDto
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}