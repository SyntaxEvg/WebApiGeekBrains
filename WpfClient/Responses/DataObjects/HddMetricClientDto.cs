
using System;

namespace MetricsManagerClient.Responses.DataObjects
{
    public class HddMetricClientDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public int AgentId { get; set; }
    }

}