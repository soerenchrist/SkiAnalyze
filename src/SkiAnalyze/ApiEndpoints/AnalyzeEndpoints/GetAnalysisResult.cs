
using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Data;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class GetAnalysisResult : BaseAsyncEndpoint
    .WithRequest<GetAnalysisResultRequest>
    .WithResponse<AnalysisResultDto>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public GetAnalysisResult(AppDbContext context,
        IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet("/api/tracks/{trackId:int}/analysis/result")]
    public override async Task<ActionResult<AnalysisResultDto>> HandleAsync([FromRoute] GetAnalysisResultRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _context.Tracks.FindAsync(request.TrackId);
        if (track == null)
            return NotFound();

        var runs = await _context.Runs
            .Where(x => x.TrackId == request.TrackId)
            .Include(x => x.Coordinates)
            .Include(x => x.Gondola!)
            .ThenInclude(x => x.Coordinates)
            .ToListAsync(cancellationToken);

        var runDtos = _mapper.Map<List<RunDto>>(runs);
        var dto = new AnalysisResultDto
        {
            Runs = runDtos,
            Bounds = runs.SelectMany(x => x.Coordinates)
                .Select(x => (ICoordinate)x)
                .GetBounds()
        };
        return Ok(dto);
    }
}
