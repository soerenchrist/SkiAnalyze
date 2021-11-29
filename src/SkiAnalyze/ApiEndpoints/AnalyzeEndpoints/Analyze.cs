using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Data;
using SkiAnalyze.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class Analyze : BaseAsyncEndpoint
    .WithRequest<AnalyzeRequest>
    .WithResponse<AnalysisStatus>
{
    private readonly IMapper _mapper;
    private readonly IBackgroundTaskQueue _taskQueue;

    public Analyze(IBackgroundTaskQueue taskQueue,
        IMapper mapper)
    {
        _mapper = mapper;
        _taskQueue = taskQueue;
    }

    [HttpPost("/api/analysis/start")]
    [SwaggerOperation(
        Summary = "Start an analysis",
        Description = "Start the analysis of a given session",
        OperationId = "Analysis.Start",
        Tags = new[] { "AnalysisEndpoints" })
    ]
    public override async Task<ActionResult<AnalysisStatus>> HandleAsync(AnalyzeRequest request, CancellationToken cancellationToken = default)
    {
        var analysisStatus = new AnalysisStatus
        {
            Id = Guid.NewGuid(),
            IsFinished = false,
        };
        await _taskQueue.QueueBackgroundWorkItemAsync(CreateWorkItem(request, analysisStatus));
        return Ok(analysisStatus);
    }

    // put the workload into the background queue to analyze the data in background
    private Func<IServiceProvider, CancellationToken, ValueTask> CreateWorkItem(AnalyzeRequest request, AnalysisStatus analysisStatus)
    {
        async ValueTask Analyze(IServiceProvider serviceProvider, CancellationToken token)
        {
            var analyzer = serviceProvider.GetRequiredService<ISessionAnalyzer>();
            await analyzer.AnalyzeSession(request.UserSessionId, analysisStatus.Id);
        }
        return Analyze;
    }
}
