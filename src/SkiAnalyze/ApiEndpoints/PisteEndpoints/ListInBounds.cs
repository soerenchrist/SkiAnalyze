using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Util;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.PisteEndpoints;

public class ListInBounds : BaseAsyncEndpoint
    .WithRequest<ListPistesInBoundsRequest>
    .WithResponse<ListPistesInBoundsResponse>
{
    private readonly IPisteSearchService _pisteSearchService;
    private readonly IMapper _mapper;

    public ListInBounds(IPisteSearchService pisteSearchService,
        IMapper mapper)
    {
        _pisteSearchService = pisteSearchService;
        _mapper = mapper;
    }

    [HttpGet("/api/pistes")]
    [SwaggerOperation(
        Summary = "Gets a list of all pistes in bounds",
        Description = "Gets a list of all pistes in a specific bound",
        OperationId = "Pistes.ListInBounds",
        Tags = new[] { "PistesEndpoints" })
    ]
    public override async Task<ActionResult<ListPistesInBoundsResponse>> HandleAsync([FromQuery] ListPistesInBoundsRequest request, CancellationToken cancellationToken = default)
    {
        var sw = new Coordinate
        {
            Latitude = request.SwLat,
            Longitude = request.SwLon
        };
        var ne = new Coordinate
        {
            Latitude = request.NeLat,
            Longitude = request.NeLon
        };
        var pistes = await _pisteSearchService.GetPistesInBounds(new Bounds
        {
            SouthWest = sw,
            NorthEast = ne
        });

        var dtos = _mapper.Map<List<PisteDto>>(pistes);
        return new ListPistesInBoundsResponse(dtos);
    }
}