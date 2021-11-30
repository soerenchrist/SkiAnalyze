namespace SkiAnalyze.ApiModels;

public class TrackDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string HexColor { get; set; } = string.Empty;
    public double TotalDistance { get; set; }
    public double TotalElevation { get; set; }
    public double MaxSpeed { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime Date { get; set; }
}
