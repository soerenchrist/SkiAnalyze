﻿using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.GondolaAggregate;

public class GondolaNode : BaseEntity<long>, ICoordinate
{
    public long OsmId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public long GondolaId { get; set; }
    public Gondola? Gondola { get; set; }
}
