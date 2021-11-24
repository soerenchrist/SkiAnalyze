using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class Get : BaseAsyncEndpoint
    .WithRequest<GetTracksRequest>
    .WithResponse<GetTracksResponse>
{
    private readonly ITracksService _tracksService;
    private readonly IMapper _mapper;

    public Get(ITracksService tracksService,
        IMapper mapper)
    {
        _tracksService = tracksService;
        _mapper = mapper;
    }

    [HttpGet("/api/tracks")]
    [SwaggerOperation(
        Summary = "Get tracks",
        Description = "Get tracks for a given user session id",
        OperationId = "Tracks.Get",
        Tags = new[] { "TrackEndpoints" })
    ]
    public override async Task<ActionResult<GetTracksResponse>> HandleAsync([FromQuery]GetTracksRequest request, CancellationToken cancellationToken = default)
    {
        var tracks = await _tracksService.GetTracks(request.UserSessionId);
        var dtos = _mapper.Map<List<TrackDto>>(tracks);

        return new GetTracksResponse(dtos, request.UserSessionId);
    }
}
