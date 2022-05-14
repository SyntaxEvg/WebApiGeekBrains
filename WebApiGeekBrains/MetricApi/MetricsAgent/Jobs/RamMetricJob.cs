﻿
using MetricsAgent.DataAccessLayer;
using MetricsAgent.Metrics;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _repository;
        private readonly PerformanceCounter _counter;
        
        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter("Memory", "% Bytes In Use");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var metrics = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow;
            
            _repository.Create(new RamMetric { Time = time, Value = metrics });
            
            return Task.CompletedTask;
        }

    }
}