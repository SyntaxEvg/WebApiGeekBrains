using MetricsManager.Controllers;
using MetricsManager.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsManagerTests
    {


        private CpuMetricsController _cpuMetricsController;


        public CpuMetricsManagerTests()
        {
            _cpuMetricsController = new CpuMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnOk()
        {
            int agentId = 1;
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);

            IActionResult result = _cpuMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = _cpuMetricsController.GetMetricsFromAgent(agentId, fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Theory]
        [InlineData(1, 0, 100, Percentile.Median)]
        [InlineData(1, 0, 100, Percentile.P75)]
        [InlineData(1, 0, 100, Percentile.P90)]
        [InlineData(1, 0, 100, Percentile.P95)]
        [InlineData(1, 0, 100, Percentile.P99)]
        public void GetMetricsByPercentileFromAgent_ReturnsOk(
            int agentId,
            int start,
            int end,
            Percentile percentile)
        {
            var fromTime = TimeSpan.FromSeconds(start);
            var toTime = TimeSpan.FromSeconds(end);

            var result = _cpuMetricsController.GetMetricsByPercentileFromAgent(agentId, fromTime, toTime, percentile);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = _cpuMetricsController.GetMetricsFromAllCluster(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Theory]
        [InlineData(0, 100, Percentile.Median)]
        [InlineData(0, 100, Percentile.P75)]
        [InlineData(0, 100, Percentile.P90)]
        [InlineData(0, 100, Percentile.P95)]
        [InlineData(0, 100, Percentile.P99)]
        public void GetMetricsByPercentileFromAllCluster_ReturnsOk(
            int start,
            int end,
            Percentile percentile)
        {
            var fromTime = TimeSpan.FromSeconds(start);
            var toTime = TimeSpan.FromSeconds(end);

            var result = _cpuMetricsController.GetMetricsByPercentileFromAllCluster(fromTime, toTime, percentile);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
