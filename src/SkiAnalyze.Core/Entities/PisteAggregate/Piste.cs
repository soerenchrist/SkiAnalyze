using OsmSharp.Complete;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.PisteAggregate;
public class Piste : BaseEntity<long>
{
    public string? Name { get; set; }
    public string? Reference { get; set; }
    public bool? Lit { get; set; }
    public bool? Snowmaking { get; set; }
    public PisteDifficulty? Difficulty { get; set; }
    public List<PisteNode> Coordinates { get; set; } = new List<PisteNode>();

    public List<TrackPoint> TrackPoints { get; set; } = new();

    public static Piste FromWay(CompleteWay way)
    {
        return new Piste
        {
            Name = way.Tags.GetNullableString("name") ?? way.Tags.GetNullableString("piste:name"),
            Reference = way.Tags.GetNullableString("ref") ?? way.Tags.GetNullableString("piste:ref"),
            Id = way.Id,
            Lit = way.Tags.GetNullableBool("piste:lit"),
            Snowmaking = way.Tags.GetNullableBool("piste:snowmaking"),
            Difficulty = ParseDifficulty(way.Tags.GetNullableString("piste:difficulty")),
            Coordinates = way.Nodes
                .Where(x => x.Id.HasValue)
                .Select(x => new PisteNode
             {
                OsmId = x.Id!.Value,
                Latitude = (float)(x.Latitude ?? 0f),
                Longitude = (float)(x.Longitude ?? 0f),
                PisteId = way.Id
            }).ToList()
        };
    }

    private static PisteDifficulty? ParseDifficulty(string? tag)
        => tag switch
        {
            "easy" => PisteDifficulty.Easy,
            "novice" => PisteDifficulty.Novice,
            "advanced" => PisteDifficulty.Advanced,
            "intermediate" => PisteDifficulty.Intermediate,
            _ => null
        };
}
