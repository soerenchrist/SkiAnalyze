namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class AddGondolaToRuns : BaseAsyncEndpoint
    .WithRequest<AddGondolaToRunsRequest>
    .WithoutResponse
{
    private readonly IRepository<Track> _trackRepository;
    private readonly IReadRepository<Gondola> _gondolaRepository;

    public AddGondolaToRuns(IRepository<Track> trackRepository,
        IReadRepository<Gondola> gondolaRepository)
    {
        _trackRepository = trackRepository;
        _gondolaRepository = gondolaRepository;
    }
    
    [HttpPost("/api/tracks/addGondola")]
    public override async Task<ActionResult> HandleAsync(AddGondolaToRunsRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        var track = await _trackRepository.GetBySpecAsync(new GetTrackWithRunsSpec(request.TrackId), cancellationToken);
        if (track == null)
            return NotFound();

        if (request.Position < 0 || request.Position > track.Runs.Count)
            return BadRequest("Position is not in range");

        var gondola = await _gondolaRepository.GetByIdAsync(request.GondolaId, cancellationToken);
        if (gondola == null)
            return NotFound();
        
        track.AddGondola(gondola, request.Position);

        await _trackRepository.UpdateAsync(track, cancellationToken);
        return Ok();
    }
}