using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.Services.Gpx;
using SkiAnalyze.Core.SessionAggregate;
using SkiAnalyze.Core.Util;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class Preview : BaseAsyncEndpoint
    .WithRequest<PreviewRequest>
    .WithResponse<MapPreviewDto>
{
    private readonly ITracksService _tracksService;
    public Preview(ITracksService tracksService)
    {
        _tracksService = tracksService;
    }

    [HttpGet("/api/analysis/preview")]
    public override async Task<ActionResult<MapPreviewDto>> HandleAsync([FromQuery] PreviewRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _tracksService.GetTrack(request.UserSessionId, request.TrackId);
        if (track == null)
            return NotFound("No track with given ids");

        var loader = new GpxFileLoader();
        var gpx = loader.LoadGpxFiles(new List<Track> { track });
        var points = gpx.ToTrackPoints();
        var bounds = points.Select(x => (ICoordinate) x).GetBounds();

        return new MapPreviewDto
        {
            Bounds = bounds,
            Coordinates = SamplePoints(points.ToList()).ToList(),
            TrackId = track.Id,
            UserSessionId = track.UserSessionId,
        };
    }

    private IEnumerable<Coordinate> SamplePoints(List<TrackPoint> trackPoints)
    {
        const int sampleRate = 3;
        for (int i = 0; i < trackPoints.Count; i += sampleRate)
        {
            yield return new Coordinate
            {
                Latitude = trackPoints[i].Latitude,
                Longitude = trackPoints[i].Longitude,
            };
        }
    }
}
