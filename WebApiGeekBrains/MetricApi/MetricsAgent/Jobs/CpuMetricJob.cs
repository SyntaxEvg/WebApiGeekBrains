using MetricsAgent.DataAccessLayer;
using MetricsAgent.Metrics;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;
        // счетчик для метрики CPU
        private PerformanceCounter _counter;
        
        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuUsageInPercents = Convert.ToInt32(_counter.NextValue());
            var time = DateTimeOffset.UtcNow;
            _repository.Create(new CpuMetric { Time = time, Value = cpuUsageInPercents });
            
            return Task.CompletedTask;
        }

    }
}