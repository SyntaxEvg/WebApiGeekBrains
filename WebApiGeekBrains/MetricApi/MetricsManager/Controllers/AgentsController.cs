using AutoMapper;
using MetricsManager.DataAccessLayer.Interfaces;
using MetricsManager.Models;
using MetricsManager.Responses;
using MetricsManager.Responses.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentInfoRepository _managerRepository;
       // private readonly AgentPool _agentsModel;
        private readonly ILogger<AgentsController> _logger;
        private readonly IMapper _mapper;
        public AgentsController(IAgentInfoRepository managerRepository, ILogger<AgentsController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _managerRepository = managerRepository;
        }


        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] MetricsManager.DataAccessLayer.Models.AgentInfo agentInfo)
        {
            _managerRepository.Create(new MetricsManager.Models.AgentInfoDto
            {
                Address = agentInfo.Address,
            });
            return Ok();
        }
              
        [HttpDelete("unregister")]
        public IActionResult UnregisterAgent([FromBody] Models.AgentInfoDto agentInfo)
        {
            _logger.LogInformation(
                $"удаление агента id:{agentInfo.Id}, address:{agentInfo.AgentAddress}");
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
            //_logger.LogInformation($"Запрос данных об агентах");

            var agents = _managerRepository.GetAgents();

            var response = new GetAgentsInfoResponse
            {
                Agents = new List<Responses.DataTransferObjects.AgentInfoDto>()
            };
            //foreach (var agent in agents)
            //{
            //    response.Agents.Add(_mapper.Map<Responses.DataTransferObjects.AgentInfoDto>(agent));
            //}
            return Ok(agents);
        }
    }
}