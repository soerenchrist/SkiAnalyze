using OsmSharp.Complete;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.SkiAreaAggregate;

public class SkiArea : BaseEntity<long>
{
    public string Name { get; set; } = string.Empty;
    public string? Wikidata { get; set; }
    public string? Website { get; set; }
    public string? AlternativeName { get; set; }
    public double CenterLatitude { get; set; }
    public double CenterLongitude { get; set; }
    public List<SkiAreaNode> Nodes { get; set; } = new List<SkiAreaNode>();
    public List<Track> Tracks { get; set; } = new();

    public static SkiArea FromWay(CompleteWay way)
    {
        var area = new SkiArea
        {
            Id = way.Id,
            Name = way.Tags.GetNullableString("name") ?? "",
            Wikidata = way.Tags.GetNullableString("wikidata"),
            Website = way.Tags.GetNullableString("website"),
            AlternativeName = way.Tags.GetNullableString("alt_name"),
            Nodes = way.Nodes
                .Where(x => x.Id.HasValue)
                .Select(x => new SkiAreaNode
                {
                    OsmId = x.Id!.Value,
                    Latitude = (float)(x.Latitude ?? 0f),
                    Longitude = (float)(x.Longitude ?? 0f),
                    SkiAreaId = way.Id
                }).ToList()
        };
        var bounds = area.Nodes.GetBounds();
        area.CenterLatitude = (bounds.SouthWest.Latitude + bounds.NorthEast.Latitude) / 2;
        area.CenterLongitude = (bounds.SouthWest.Longitude + bounds.NorthEast.Longitude) / 2;

        return area;
    }
}
