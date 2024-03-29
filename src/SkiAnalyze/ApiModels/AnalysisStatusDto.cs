﻿namespace SkiAnalyze.ApiModels;

public class AnalysisStatusDto
{
    public bool IsFinished { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public int TrackId { get; set; }
}
