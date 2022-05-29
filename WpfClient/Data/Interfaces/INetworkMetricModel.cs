using System;
using System.Collections.Generic;
using MetricsManagerClient.Responses.DataObjects;

namespace MetricsManagerClient.Data.Interfaces
{
    public interface INetworkMetricModel
    {
        public Queue<NetworkMetricClientDto> Metrics { get; set; }
        public DateTimeOffset LastAddedTime { get; }
        void AddMetrics(List<NetworkMetricClientDto> recievedMetrics);
    }
}