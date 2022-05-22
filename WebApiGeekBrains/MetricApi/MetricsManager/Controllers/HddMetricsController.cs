using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MetricsManager.Responses.DataTransferObjects;
using MetricsManager.DataAccessLayer.Interfaces;
using AutoMapper;
using MetricsManager.Responses;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IDotNetMetricsManagerRepository _managerRepository;
        private readonly IMapper _mapper;
        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public GetByPeriodHddMetricsApiResponse GetMetricsFromAgent(
             [FromRoute] int agentId,
             [FromRoute] DateTimeOffset fromTime,
             [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Агент: {agentId}, From:{fromTime}, To:{toTime}");

            var metrics = _managerRepository.GetByTimePeriodFromAgent(fromTime, toTime, agentId);

            var response = new GetByPeriodHddMetricsApiResponse
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            return response;
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public GetByPeriodHddMetricsApiResponse GetMetricsFromAllCluster(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Общие данные From:{fromTime}, To:{toTime}");

            var metrics = _managerRepository.GetByTimePeriod(fromTime, toTime);

            var response = new GetByPeriodHddMetricsApiResponse
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            return response;
        }
    }
}
