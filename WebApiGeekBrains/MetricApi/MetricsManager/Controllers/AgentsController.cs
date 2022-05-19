using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly AgentPool _agentsModel;
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(AgentPool agentsModel, ILogger<AgentsController> logger)
        {
            _agentsModel = agentsModel;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в AgentsController");
        }


        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation(
                $"Регистрация агента id:{agentInfo.AgentId}, address:{agentInfo.AgentAddress}");
            return Ok();
        }
              
        [HttpDelete("unregister")]
        public IActionResult UnregisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation(
                $"удаление агента id:{agentInfo.AgentId}, address:{agentInfo.AgentAddress}");
            return Ok();
        }
        
        
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Активация агента id:{agentId}");
            return Ok();
        }
        
        
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Деактивация агента id:{agentId}");
            return Ok();
        }
        
        
        [HttpGet("get_agents")]
        public IActionResult GetRegisterAgents()
        {
            _logger.LogInformation($"Запрос данных об агентах");
            return Ok(_agentsModel.Values);
        }
    }
}