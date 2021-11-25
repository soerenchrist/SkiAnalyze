using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class Remove : BaseAsyncEndpoint
    .WithRequest<RemoveTrackRequest>
    .WithoutResponse
{
    private readonly ITracksService _tracksService;
    public Remove(ITracksService tracksService)
    {
        _tracksService = tracksService;
    }

    [HttpDelete("/api/tracks")]
    [SwaggerOperation(
        Summary = "Remove a track",
        Description = "Remove a track from the session",
        OperationId = "Tracks.Remove",
        Tags = new[] { "TrackEndpoints" })
    ]
    public override async Task<ActionResult> HandleAsync([FromQuery] RemoveTrackRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _tracksService.GetTrack(request.UserSessionId, request.TrackId);
        if (track == null)
            return NotFound();

        await _tracksService.RemoveTrack(track);
        return Ok();
    }
}
