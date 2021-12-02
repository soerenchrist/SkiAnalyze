using OsmSharp.Complete;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.GondolaAggregate;

public class Gondola : BaseEntity<long>
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
    public List<GondolaNode> Coordinates { get; set; } = new();

    public List<Run> Runs { get; set; } = new();

    public static Gondola FromWay(CompleteWay way)
    {
        return new Gondola
        {
            Name = way.Tags.GetNullableString("name"),
            Type = way.Tags.GetString("aerialway"),
            Occupancy = way.Tags.GetNullableInt("aerialway:occupancy"),
            Capacity = way.Tags.GetNullableInt("aerialway:capacity"),
            Duration = way.Tags.GetNullableInt("aerialway:duration"),
            Bubble = way.Tags.GetNullableBool("aerialway:bubble"),
            Heating = way.Tags.GetNullableBool("aerialway:heating"),
            OneWay = way.Tags.GetNullableBool("oneway"),
            Reference = way.Tags.GetNullableString("ref"),
            Description = way.Tags.GetNullableString("description"),
            Id = way.Id,
            Wikidata = way.Tags.GetNullableString("wikidata"),
            Wikipedia = way.Tags.GetNullableString("wikipedia"),
            Coordinates = way.Nodes
                .Where(x => x.Id.HasValue)
                .Select(x => new GondolaNode
            {
                OsmId = x.Id!.Value,
                Latitude = (float)(x.Latitude ?? 0f),
                Longitude = (float)(x.Longitude ?? 0f),
                GondolaId = way.Id
            }).ToList()
        };
    }
}
