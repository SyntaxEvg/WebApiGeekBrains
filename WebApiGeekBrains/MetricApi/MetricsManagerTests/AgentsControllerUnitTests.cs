using MetricsManager.Controllers;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private readonly AgentsController _controller;

        public AgentsControllerUnitTests()
        {
            var agentsModel = new AgentPool();
            var loggerMock = new Mock<ILogger<AgentsController>>();

            _controller = new AgentsController(agentsModel, loggerMock.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            var agentInfo = new AgentInfo();
            var result = _controller.RegisterAgent(agentInfo);
            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void UnregisterAgent_ReturnsOk()
        {
            var agentInfo = new AgentInfo();

            var result = _controller.UnregisterAgent(agentInfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var agentId = 1;
           var result = _controller.EnableAgentById(agentId);
           Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var agentId = 1;
            var result = _controller.DisableAgentById(agentId);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetRegisterAgents_ReturnsOk()
        {

            var result = _controller.GetRegisterAgents();
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}