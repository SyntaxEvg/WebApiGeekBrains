using System;
using System.Collections.Generic;
using MetricsManagerClient.Responses.DataObjects;

namespace MetricsManagerClient.Data.Interfaces
{
    public interface ICpuMetricModel
    {
        public Queue<CpuMetricClientDto> Metrics { get; set; }
        public DateTimeOffset LastAddedTime { get; }
        void AddMetrics(List<CpuMetricClientDto> recievedMetrics);
        
       
    }
}