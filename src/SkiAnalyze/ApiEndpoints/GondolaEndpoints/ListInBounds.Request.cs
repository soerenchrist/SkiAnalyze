﻿namespace SkiAnalyze.ApiEndpoints.GondolaEndpoints;

public class ListGondolasInBoundsRequest
{
    public const string Route = "/gondolas";

    public float NeLat { get; set; }
    public float NeLon { get; set; }
    public float SwLat { get; set; }
    public float SwLon { get; set; }
}

