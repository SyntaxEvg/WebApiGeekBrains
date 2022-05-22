using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.DataAccessLayer.Interfaces;
using MetricsManager.Enum;
using MetricsManager.Responses;
using MetricsManager.Responses.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsManagerRepository _managerRepository;
        private readonly IMapper _mapper;
        public CpuMetricsController(ICpuMetricsManagerRepository managerRepository,ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
          [FromRoute] int agentId,
          [FromRoute] DateTimeOffset fromTime,
          [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Агент: {agentId}, From:{fromTime}, To:{toTime}");

            var metrics = _managerRepository.GetByTimePeriodFromAgent(fromTime, toTime, agentId);

            var response = new GetByPeriodCpuMetricsApiResponse
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
           [FromRoute] DateTimeOffset fromTime,
           [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Общие данные From:{fromTime}, To:{toTime}");

            var metrics = _managerRepository.GetByTimePeriod(fromTime, toTime);

            var response = new GetByPeriodCpuMetricsApiResponse
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId,
            [FromRoute] TimeSpan fromTime,
            [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Агент: {agentId}, From:{fromTime}, To:{toTime}");
            return Ok();
        }

        
        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        //public IActionResult GetMetricsByPercentileFromAgent(
        //    [FromRoute] int agentId, 
        //    [FromRoute] TimeSpan fromTime, 
        //    [FromRoute] TimeSpan toTime,
        //    [FromRoute] Percentile percentile)
        //{
        //    _logger.LogInformation($"Агент: {agentId}, From:{fromTime}, To:{toTime}, Percentile:{percentile}");
        //    return Ok();
        //}



        
        //[HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAllCluster(
        //    [FromRoute] TimeSpan fromTime, 
        //    [FromRoute] TimeSpan toTime)
        //{
        //    _logger.LogInformation($"Общие данные From:{fromTime}, To:{toTime}");
        //    return Ok();
        //}

        
        //[HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        //public IActionResult GetMetricsByPercentileFromAllCluster(
        //    [FromRoute] TimeSpan fromTime, 
        //    [FromRoute] TimeSpan toTime,
        //    [FromRoute] Percentile percentile)
        //{
        //    _logger.LogInformation($"Общие данные From:{fromTime}, To:{toTime}, Percentile:{percentile}");
        //    return Ok();
        //}
    }

}