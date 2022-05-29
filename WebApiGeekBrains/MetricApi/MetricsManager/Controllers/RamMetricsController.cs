using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.Responses.DataTransferObjects;
using MetricsManager.DataAccessLayer.Interfaces;
using MetricsManager.Responses;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IDotNetMetricsManagerRepository _managerRepository;
        private readonly IMapper _mapper;
        public RamMetricsController(ILogger<RamMetricsController> logger, IDotNetMetricsManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
               _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public GetByPeriodRamMetricsApiResponse GetMetricsFromAgent(
           [FromRoute] int agentId,
           [FromRoute] DateTimeOffset fromTime,
           [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Агент: {agentId}, From:{fromTime}, To:{toTime}");

            var metrics = _managerRepository.GetByTimePeriodFromAgent(fromTime, toTime, agentId);

            var response = new GetByPeriodRamMetricsApiResponse
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return response;
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public GetByPeriodRamMetricsApiResponse GetMetricsFromAllCluster(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            //_logger.LogInformation($"Общие данные From:{fromTime}, To:{toTime}");

            var metrics = _managerRepository.GetByTimePeriod(fromTime, toTime);

            var response = new GetByPeriodRamMetricsApiResponse
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return response;
        }
    }
}