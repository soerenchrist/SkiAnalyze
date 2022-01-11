namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class RemoveRun : BaseAsyncEndpoint
    .WithRequest<RemoveRunRequest>
    .WithoutResponse
{
    private readonly IRepository<Track> _trackRepository;

    public RemoveRun(IRepository<Track> trackRepository)
    {
        _trackRepository = trackRepository;
    }
    
    [HttpDelete("/api/tracks/{trackId:int}/runs/{runId:int}")]
    public override async Task<ActionResult> HandleAsync([FromRoute] RemoveRunRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var track = await _trackRepository.GetBySpecAsync(new GetTrackWithRunsSpec(request.TrackId), cancellationToken);
        if (track == null)
            return NotFound();
        
        var runToDelete = track.Runs.FirstOrDefault(x => x.Id == request.RunId);
        if (runToDelete == null)
            return NotFound();

        track.RemoveRun(runToDelete);
        
        track.UpdateRunNumbers();
        track.UpdateStats();
        await _trackRepository.UpdateAsync(track, cancellationToken);

        return Ok();
    }
}