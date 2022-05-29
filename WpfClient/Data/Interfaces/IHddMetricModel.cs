using System;
using System.Collections.Generic;
using MetricsManagerClient.Responses.DataObjects;

namespace MetricsManagerClient.Data.Interfaces
{
    public interface IHddMetricModel
    {
        public Queue<HddMetricClientDto> Metrics { get; set; }
        public DateTimeOffset LastAddedTime { get; }
        
        void AddMetrics(List<HddMetricClientDto> recievedMetrics);
    }
}