using SkiAnalyze.SharedKernel;

namespace SkiAnalayze.Core.PisteAggregate;
public class PisteCoordinate : BaseEntity<long>
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public override string ToString()
    {
        return $"Latitude: {Latitude}, Longitude: {Longitude}";
    }
}
