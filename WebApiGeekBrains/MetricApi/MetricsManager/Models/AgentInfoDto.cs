using System;

namespace MetricsManager.Models
{
    public class AgentInfoDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public Uri AgentAddress { get; set; }
        public bool Enable { get; set; }
    }
}
