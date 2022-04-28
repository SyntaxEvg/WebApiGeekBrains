using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        [HttpGet("agent/{agentId}/errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
              [FromRoute] int agentId,
              [FromRoute] TimeSpan fromTime,
              [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }


        [HttpGet("cluster/errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
