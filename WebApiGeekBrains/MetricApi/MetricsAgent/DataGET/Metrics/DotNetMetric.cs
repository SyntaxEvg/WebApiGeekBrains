using System;

namespace MetricsAgent.Metrics
{
    public class DotNetMetric
    {
        public int Id { get; set; } 
        public float Value { get; set; } 
        public DateTimeOffset Time { get; set; }
    }
}