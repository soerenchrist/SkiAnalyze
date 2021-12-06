using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Data;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class Preview : BaseAsyncEndpoint
    .WithRequest<PreviewRequest>
    .WithResponse<MapPreviewDto>
{
    private readonly AppDbContext _context;
    public Preview(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("/api/tracks/{trackId}/analysis/preview")]
    public override async Task<ActionResult<MapPreviewDto>> HandleAsync([FromRoute] PreviewRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _context.Tracks.FindAsync(request.TrackId);
        if (track == null)
            return NotFound($"No track with given id {request.TrackId}");

        var status = await _context.AnalysisStatus.FirstOrDefaultAsync(x => x.TrackId == track.Id);

        if (status == null || !status.IsFinished)
        {
            if (track.FileType != TrackFileType.Gpx)
                return NotFound();
            var loader = new GpxFileLoader();
            var gpx = loader.LoadGpxFile(track);
            var points = gpx.ToTrackPoints();
            var bounds = points.Select(x => (ICoordinate)x).GetBounds();
            return new MapPreviewDto
            {
                Bounds = bounds,
                Coordinates = SamplePoints(points.ToList()).ToList(),
                TrackId = track.Id,
                Color = "#00ff00"
            };
        }

        var trackPoints = await _context.TrackPoints
            .Include(x => x.Run)
            .Where(x => x.Run!.TrackId == track.Id)
            .ToListAsync();

        var dbPoints = SamplePoints(trackPoints).ToList();
        var dbBounds = dbPoints.Select(x => (ICoordinate)x).GetBounds();
        return new MapPreviewDto
        {
            Bounds = dbBounds,
            Coordinates = dbPoints.ToList(),
            TrackId = track.Id,
            Color = track.HexColor
        };
    }

    private IEnumerable<Coordinate> SamplePoints(List<TrackPoint> trackPoints)
    {
        const int sampleRate = 5;
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
