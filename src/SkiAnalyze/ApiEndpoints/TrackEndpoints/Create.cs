using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.SessionAggregate;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class Create : BaseAsyncEndpoint
    .WithRequest<CreateTrackRequest>
    .WithResponse<CreateTrackResponse>
{
    private readonly ITracksService _tracksService;
    private readonly IMapper _mapper;

    public Create(ITracksService tracksService,
        IMapper mapper)
    {
        _mapper = mapper;
        _tracksService = tracksService;
    }

    [HttpPost("/api/tracks")]
    [SwaggerOperation(
        Summary = "Create a new track",
        Description = "Create a new track",
        OperationId = "Tracks.Create",
        Tags = new[] { "TrackEndpoints" })
    ]
    public override async Task<ActionResult<CreateTrackResponse>> HandleAsync(CreateTrackRequest request, CancellationToken cancellationToken = default)
    {
        var track = new Track
        {
            Name = request.Name,
            HexColor = request.Color,
            GpxFileContents = request.GpxFileContent,
            UserSessionId = request.UserSessionId ?? Guid.Empty
        };

        var result = await _tracksService.AddTrack(track);
        var dto = _mapper.Map<TrackDto>(result);
        var response = new CreateTrackResponse(dto, result.UserSessionId);

        return response;
    }
}
