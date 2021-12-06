namespace SkiAnalyze.ApiModels;

public class AnalysisResultDto
{
    public Bounds Bounds { get; set; } = new();
    public List<RunDto> Runs { get; set; } = new List<RunDto>();
}

public class RunDto
{
    public int Number { get; set; }
    public bool Downhill { get; set; }
    public GondolaDto? Gondola { get; set; }
    public List<TrackPointDto> Coordinates { get; set; } = new();
    public double TotalDistance { get; set; }
    public double TotalElevation { get; set; }
    public double AverageSpeed { get; set; }
    public double MaxSpeed { get; set; }
    public double AverageHeartRate { get; set; }
    public int MaxHeartRate { get; set; }
    public string Color { get; set; } = string.Empty;
    public int TrackId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class TrackPointDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime DateTime { get; set; }
    public double? Elevation { get; set; }
    public long? PisteId { get; set; }
    public int RunId { get; set; }
}