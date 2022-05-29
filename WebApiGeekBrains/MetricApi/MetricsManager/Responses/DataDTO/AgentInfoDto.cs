
using System;

namespace MetricsManager.Responses.DataTransferObjects
{
    public class AgentInfoDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public Uri AgentAddress { get; set; }
        public bool Enable { get; set; }
    }
}