﻿using System;
using System.Collections.Generic;
using System.Linq;
using MetricsManagerClient.Data.Interfaces;
using MetricsManagerClient.Responses.DataObjects;
using Microsoft.Extensions.Logging;

namespace MetricsManagerClient.Data
{
    public class RamMetricModel : IRamMetricModel
    {
        public Queue<RamMetricClientDto> Metrics { get; set; }

        private readonly ILogger _logger;
        private readonly int _metricsLimit;
        public RamMetricModel(ILogger<IRamMetricModel> logger)
        {
            _logger = logger;
            Metrics = new Queue<RamMetricClientDto>();
            _metricsLimit = DefaultData.RamMetric.Count;
            AddMetrics(DefaultData.RamMetric);
        }

        public DateTimeOffset LastAddedTime => 
            Metrics?.Last().Time 
            ?? DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 86_400);

        public void AddMetrics(List<RamMetricClientDto> recievedMetrics)
        {
            if (recievedMetrics.Count == 0)
                return;

            _logger.LogDebug($"Adding {recievedMetrics.Count} metrics");
            
            recievedMetrics.ForEach(metric =>
            {
                if (Metrics.Count == _metricsLimit)
                {
                    Metrics.Dequeue();
                }
                Metrics.Enqueue(metric);
            });
        }
    }
}