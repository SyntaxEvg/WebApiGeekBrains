using System;

namespace MetricsManagerClient.Data.Models
{
    public class CpuMetricClient
    {
        public int Id { get; set; } 
        public int Value { get; set; } 
        public DateTimeOffset Time { get; set; }
    }
}