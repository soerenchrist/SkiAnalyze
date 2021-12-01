namespace SkiAnalyze.Blazor.Models.Maps;

public class TileLayerOptions 
{
    public string Layer { get; }
    public string? Attribution { get; set; }
    public int MaxZoom { get; set; } = 18;

    public TileLayerOptions(string layer)
    {
        Layer = layer;
    }
}