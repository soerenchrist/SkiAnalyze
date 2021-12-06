namespace SkiAnalyze.ApiModels;

public class TrackDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string HexColor { get; set; } = string.Empty;
    public double TotalDistance { get; set; }
    public double TotalElevation { get; set; }
    public double MaxSpeed { get; set; }
    public double AverageSpeed { get; set; }
    public double? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public SkiAreaDto? SkiArea { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime Date { get; set; }
}
