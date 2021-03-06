using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class StartAnalysis : BaseAsyncEndpoint
    .WithRequest<StartAnalysisRequest>
    .WithResponse<AnalysisStatusDto>
{
    private readonly IMapper _mapper;
    private readonly IBackgroundTaskQueue _taskQueue;

    public StartAnalysis(IBackgroundTaskQueue taskQueue,
        IMapper mapper)
    {
        _mapper = mapper;
        _taskQueue = taskQueue;
    }

    [HttpPost("/api/tracks/{trackId:int}/analysis/start")]
    [SwaggerOperation(
        Summary = "Start an analysis",
        Description = "Start the analysis of a given session",
        OperationId = "Analysis.Start",
        Tags = new[] { "AnalysisEndpoints" })
    ]
    public override async Task<ActionResult<AnalysisStatusDto>> HandleAsync([FromRoute] StartAnalysisRequest request, CancellationToken cancellationToken = default)
    {
        var analysisStatus = new AnalysisStatus
        {
            TrackId = request.TrackId,
            IsFinished = false,
        };
        await _taskQueue.QueueBackgroundWorkItemAsync(CreateWorkItem(request, analysisStatus));
        var dto = _mapper.Map<AnalysisStatusDto>(analysisStatus);
        return Ok(dto);
    }

    // put the workload into the background queue to analyze the data in background
    private Func<IServiceProvider, CancellationToken, ValueTask> CreateWorkItem(StartAnalysisRequest request, AnalysisStatus analysisStatus)
    {
        async ValueTask Analyze(IServiceProvider serviceProvider, CancellationToken token)
        {
            var analyzer = serviceProvider.GetRequiredService<IAnalyzer>();
            await analyzer.AnalyzeTrack(request.TrackId);
        }
        return Analyze;
    }
}
