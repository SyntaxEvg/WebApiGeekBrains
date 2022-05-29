using System;

namespace WpfClient.Requests
{
    public class GetCpuMetricsFromAgentRequest
    {
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}