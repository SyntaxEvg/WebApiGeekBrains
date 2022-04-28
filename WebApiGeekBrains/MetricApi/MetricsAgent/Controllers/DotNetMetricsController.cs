using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCountMetrics([FromRoute] TimeSpan fromTime,  [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
