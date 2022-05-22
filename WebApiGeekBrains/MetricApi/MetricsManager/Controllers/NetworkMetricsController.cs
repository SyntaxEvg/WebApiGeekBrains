using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.Responses.DataTransferObjects;
using MetricsManager.DataAccessLayer.Interfaces;
using MetricsManager.Responses;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IDotNetMetricsManagerRepository _managerRepository;
        private readonly IMapper _mapper;
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public GetByPeriodNetworkMetricsApiResponse GetMetricsFromAgent(
           [FromRoute] int agentId,
           [FromRoute] DateTimeOffset fromTime,
           [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Агент: {agentId}, From:{fromTime}, To:{toTime}");

            var metrics = _managerRepository.GetByTimePeriodFromAgent(fromTime, toTime, agentId);

            var response = new GetByPeriodNetworkMetricsApiResponse
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return response;
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public GetByPeriodNetworkMetricsApiResponse GetMetricsFromAllCluster(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Общие данные From:{fromTime}, To:{toTime}");

            var metrics = _managerRepository.GetByTimePeriod(fromTime, toTime);

            var response = new GetByPeriodNetworkMetricsApiResponse
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return response;
        }
    }
}