namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class Preview : BaseAsyncEndpoint
    .WithRequest<PreviewRequest>
    .WithResponse<MapPreviewDto>
{
    private readonly IReadRepository<Track> _tracksRepository;
    public Preview(IReadRepository<Track> tracksRepository)
    {
        _tracksRepository = tracksRepository;
    }

    [HttpGet("/api/tracks/{trackId}/analysis/preview")]
    public override async Task<ActionResult<MapPreviewDto>> HandleAsync([FromRoute] PreviewRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _tracksRepository.GetBySpecAsync(new GetCompleteTrackSpec(request.TrackId));
        if (track == null)
            return NotFound($"No track with given id {request.TrackId}");

        if (track.AnalysisStatus == null || !track.AnalysisStatus.IsFinished)
        {
            var loader = new GpxFileLoader();
            var gpx = loader.LoadGpxFile(track);
            var points = gpx.ToTrackPoints();
            var bounds = points.Select(x => (ICoordinate)x).GetBounds();
            return new MapPreviewDto
            {
                Bounds = bounds,
                Coordinates = SamplePoints(points.ToList()).ToList(),
                TrackId = track.Id,
            };
        }

        var dbPoints = SamplePoints(track.Runs.SelectMany(x => x.Coordinates).ToList());
        var dbBounds = dbPoints.Select(x => (ICoordinate)x).GetBounds();
        return new MapPreviewDto
        {
            Bounds = dbBounds,
            Coordinates = dbPoints.ToList(),
            TrackId = track.Id,
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
