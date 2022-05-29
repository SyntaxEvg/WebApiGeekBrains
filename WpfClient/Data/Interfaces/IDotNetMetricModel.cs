using System;
using System.Collections.Generic;
using MetricsManagerClient.Responses.DataObjects;

namespace MetricsManagerClient.Data.Interfaces
{
    public interface IDotNetMetricModel
    {
        public Queue<DotNetMetricClientDto> Metrics { get; set; }
        public DateTimeOffset LastAddedTime { get; }
        void AddMetrics(List<DotNetMetricClientDto> recievedMetrics);
    }
}