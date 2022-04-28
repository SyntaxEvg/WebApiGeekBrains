using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("left")]
        public IActionResult GetRemainingFreeDiskSpaceMetrics()
        {
            return Ok();
        }
    }
}
