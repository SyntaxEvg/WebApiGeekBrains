﻿using MetricsManager.Controllers;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;

namespace MetricsManagerTests
{
    public class AgentsControllerTests
    {
        private AgentsController _agentsController;
        private AgentPool _agentPool;


        public AgentsControllerTests()
        {
            _agentPool = LazyAgentPool.Instance;
            _agentsController = new AgentsController(_agentPool);
        }

        [Fact]
        [Priority(2)]
        public void GetAgentsTest()
        {
            IActionResult actionResult = _agentsController.GetAllAgents();
            OkObjectResult result = Assert.IsAssignableFrom<OkObjectResult>(actionResult);

            //(IEnumerable<AgentInfo>)result.Value 
            //result.Value as IEnumerable<AgentInfo>
            Assert.NotNull(result.Value as IEnumerable<AgentInfo>);
            Assert.NotEmpty((IEnumerable<AgentInfo>)result.Value);
        }

        [Theory]
        [Priority(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public void RegisterAgentTest(int agentId)
        {
            AgentInfo agentInfo = new AgentInfo() { AgentId = agentId, Enable = true };
            IActionResult actionResult = _agentsController.RegisterAgent(agentInfo);
            Assert.IsAssignableFrom<IActionResult>(actionResult);
        }
        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            var agentInfo = new AgentInfo();

            var result = _agentsController.RegisterAgent(agentInfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void UnregisterAgent_ReturnsOk()
        {
            var agentInfo = new AgentInfo();

            var result = _agentsController.UnregisterAgent(agentInfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = _agentsController.EnableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = _agentsController.DisableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetRegisterAgents_ReturnsOk()
        {

            var result = _agentsController.GetRegisterAgents();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


    }
}
