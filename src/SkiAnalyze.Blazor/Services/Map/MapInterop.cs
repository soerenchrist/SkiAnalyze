using Microsoft.JSInterop;
using SkiAnalyze.Blazor.Models.Maps;

namespace SkiAnalyze.Blazor.Services.Map;

public class MapInterop 
{
    private readonly IJSRuntime _js;
    public MapInterop(IJSRuntime js)
    {
        _js = js;
    }
    public ValueTask DisplayMap(string mapName, MapOptions mapOptions, TileLayerOptions tileLayerOptions) 
        => _js.InvokeVoidAsync("displayMap", mapName, mapOptions, tileLayerOptions);
    
    public ValueTask SetPolylines(List<Polyline> polylines) 
        => _js.InvokeVoidAsync("setPolylines", polylines);
}