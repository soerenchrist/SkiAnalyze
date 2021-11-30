namespace SkiAnalyze.ApiModels;

public class TrackDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string HexColor { get; set; } = string.Empty;
    public double TotalDistance { get; set; }
    public double TotalElevation { get; set; }
    public double MaxSpeed { get; set; }
}
