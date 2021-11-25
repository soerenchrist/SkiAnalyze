using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.GondolaAggregate;
using AutoMapper;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Util;
using Ardalis.Result.AspNetCore;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.GondolaEndpoints;

public class ListInBounds : BaseAsyncEndpoint
    .WithRequest<ListGondolasInBoundsRequest>
    .WithResponse<ListGondolasInBoundsResponse>
{
    private readonly IGondolaSearchService _gondolaSearchService;
    private readonly IMapper _mapper;

    public ListInBounds(IGondolaSearchService gondolaSearchService,
        IMapper mapper)
    {

        _gondolaSearchService = gondolaSearchService;
        _mapper = mapper;
    }

    [HttpGet("/api/gondolas")]
    [SwaggerOperation(
        Summary = "Gets a list of all gondolas in bounds",
        Description = "Gets a list of all gondolas in a specific bound",
        OperationId = "Gondolas.ListInBounds",
        Tags = new[] { "GondolaEndpoints" })
    ]
    public override async Task<ActionResult<ListGondolasInBoundsResponse>> HandleAsync([FromQuery] ListGondolasInBoundsRequest request, 
        CancellationToken cancellationToken = default)
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
        var gondolas = await _gondolaSearchService.GetGondolasInBounds(new Bounds
        {
            SouthWest = sw,
            NorthEast = ne
        });

        var dtos = _mapper.Map<List<GondolaDto>>(gondolas);
        return new ListGondolasInBoundsResponse(dtos);
    }
}
