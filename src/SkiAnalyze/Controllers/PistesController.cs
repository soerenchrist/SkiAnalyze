using Microsoft.AspNetCore.Mvc;
using SkiAnalayze.Core.PisteAggregate;

namespace SkiAnalyze.Controllers;
[ApiController]
[Route("pistes")]
public class PistesController : ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<Piste>> GetPistes(
        [FromQuery] float nwLat, 
        [FromQuery] float nwLon,
        [FromQuery] float seLat,
        [FromQuery] float seLon)
    {
        var pistes = new List<Piste>();
        return Ok(pistes);
    }
}
