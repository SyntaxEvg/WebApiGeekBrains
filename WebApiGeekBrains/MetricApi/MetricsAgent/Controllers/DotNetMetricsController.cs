using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsAgent.DataAccessLayer;
using MetricsAgent.Metrics;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(IMapper mapper ,IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _logger.LogInformation(1, "NLog встроен в DotNetMetricsController");
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _logger.LogInformation($"Создается запись с данными Time:{request.Time}; Value:{request.Value}");

            _repository.Create(new DotNetMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTimePeriod(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос записи From:{fromTime}; To:{toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new GetByPeriodDotNetMetricsResponse
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));

                //response.Metrics.Add(new DotNetMetricDto
                //{
                //    Time = metric.Time, 
                //    Value = metric.Value, 
                //    Id = metric.Id
                //});
            }

            return Ok(response);
        }
    }
}