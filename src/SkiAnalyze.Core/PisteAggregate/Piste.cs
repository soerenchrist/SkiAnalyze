using OsmSharp.Complete;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalayze.Core.PisteAggregate;
public class Piste : BaseEntity<long>, IAggregateRoot
{
    public string? Name { get; set; }
    public string? Reference { get; set; }
    public bool? Lit { get; set; }
    public bool? Snowmaking { get; set; }
    public PisteDifficulty? Difficulty { get; set; }
    public List<PisteCoordinate> Coordinates { get; set; } = new List<PisteCoordinate>();

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
                .Select(x => new PisteCoordinate
             {
                    Id = x.Id!.Value,
                Latitude = (float)(x.Latitude ?? 0f),
                Longitude = (float)(x.Longitude ?? 0f)
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
