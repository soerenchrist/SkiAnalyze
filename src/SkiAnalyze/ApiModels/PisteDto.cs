﻿using SkiAnalyze.Core.Entities.PisteAggregate;

namespace SkiAnalyze.ApiModels;

public class PisteDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Reference { get; set; }
    public bool? Lit { get; set; }
    public bool? Snowmaking { get; set; }
    public double Length { get; set; }
    public PisteDifficulty? Difficulty { get; set; }
    public List<PisteNodeDto> Coordinates { get; set; } = new List<PisteNodeDto>();
}

public class PisteNodeDto
{
    public long OsmId { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
}
