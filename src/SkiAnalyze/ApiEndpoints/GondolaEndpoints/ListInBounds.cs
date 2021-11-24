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
    .WithRequest<ListInBoundsRequest>
    .WithResponse<ListInBoundsResponse>
{
    private readonly IGondolaSearchService _gondolaSearchService;
    private readonly IMapper _mapper;

    public ListInBounds(IGondolaSearchService gondolaSearchService,
        IMapper mapper)
    {
        _gondolaSearchService = gondolaSearchService;
        _mapper = mapper;
    }

    [HttpGet("/gondolas")]
    [SwaggerOperation(
        Summary = "Gets a list of all gondolas in bounds",
        Description = "Gets a list of all gondolas in a specific bound",
        OperationId = "Gondolas.ListInBounds",
        Tags = new[] { "GondolaEndpoints" })
    ]
    public override async Task<ActionResult<ListInBoundsResponse>> HandleAsync([FromQuery] ListInBoundsRequest request, CancellationToken cancellationToken = default)
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
        var result = await _gondolaSearchService.GetGondolasInBounds(sw, ne);
        if (result.IsSuccess)
        {
            return Ok(ToResponse(result.Value));
        }

        var newResult = result.ToResult<ListInBoundsResponse, List<Gondola>>();
        return this.ToActionResult(newResult);
    }

    private ListInBoundsResponse ToResponse(List<Gondola> value)
    {
        var dtos = _mapper.Map<List<GondolaDto>>(value);
        return new ListInBoundsResponse
        {
            Gondolas = dtos
        };
    }
}
