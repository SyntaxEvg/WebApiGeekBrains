using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using MetricsManagerClient.Data.Interfaces;
using MetricsManagerClient.Responses.DataObjects;

namespace MetricsManagerClient.Data
{
    public class CpuMetricModel : ICpuMetricModel
    {
	    public Queue<CpuMetricClientDto> Metrics { get; set; }

	    private readonly ILogger _logger;
	    private readonly int _metricsLimit;
		public CpuMetricModel(ILogger<ICpuMetricModel> logger)
		{
			_logger = logger;
			Metrics = new Queue<CpuMetricClientDto>();
			_metricsLimit = DefaultData.CpuMetric.Count;
			AddMetrics(DefaultData.CpuMetric);
		}

		public DateTimeOffset LastAddedTime => 
			Metrics?.Last().Time 
			?? DateTimeOffset.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 86_400);

		public void AddMetrics(List<CpuMetricClientDto> recievedMetrics)
		{
			if (recievedMetrics == null  || recievedMetrics.Count == 0)
				return;
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