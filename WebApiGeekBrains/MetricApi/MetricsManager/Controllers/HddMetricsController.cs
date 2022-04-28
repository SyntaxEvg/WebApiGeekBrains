using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        [HttpGet("agent/{agentId}/left")]
        public IActionResult GetMetricsFromAgent(
             [FromRoute] int agentId)
        {
            return Ok();
        }


        [HttpGet("cluster/left")]
        public IActionResult GetMetricsFromAllCluster()
        {
            return Ok();
        }
    }
}
