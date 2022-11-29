using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class Remove : EndpointBaseAsync
    .WithRequest<RemoveTrackRequest>
    .WithoutResult
{
    private readonly IRepository<Track> _tracksRepository;
    public Remove(IRepository<Track> tracksRepository)
    {
        _tracksRepository = tracksRepository;
    }

    [HttpDelete("/api/tracks/{trackId}")]
    [SwaggerOperation(
        Summary = "Remove a track",
        Description = "Remove a track from the session",
        OperationId = "Tracks.Remove",
        Tags = new[] { "TrackEndpoints" })
    ]
    public override async Task<ActionResult> HandleAsync([FromRoute] RemoveTrackRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _tracksRepository.GetByIdAsync(request.TrackId);
        if (track == null)
            return NotFound();

        await _tracksRepository.DeleteAsync(track);
        return Ok();
    }
}
