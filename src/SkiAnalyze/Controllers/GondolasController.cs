using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.Controllers;
[ApiController]
[Route("gondolas")]
public class GondolasController : ControllerBase
{
    
    public GondolasController()
    {
    }

    [HttpGet]
    public ActionResult<IEnumerable<Gondola>> GetGondolas(
        [FromQuery] float nwLat, 
        [FromQuery] float nwLon,
        [FromQuery] float seLat,
        [FromQuery] float seLon)
    {
        return Ok(new List<Gondola>());
    }
}
