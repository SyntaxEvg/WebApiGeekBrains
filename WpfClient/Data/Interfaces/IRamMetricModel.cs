using System;
using System.Collections.Generic;
using MetricsManagerClient.Responses.DataObjects;

namespace MetricsManagerClient.Data.Interfaces
{
    public interface IRamMetricModel
    {
        public Queue<RamMetricClientDto> Metrics { get; set; }
        public DateTimeOffset LastAddedTime { get; }       
        void AddMetrics(List<RamMetricClientDto> recievedMetrics);
    }
}