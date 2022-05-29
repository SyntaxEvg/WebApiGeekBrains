
using System;

namespace MetricsManager.DataAccessLayer.Models
{
    public class AgentInfo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int AgentId { get; set; }
        public string Address { get; set; }
        public Uri AgentAddress { get; set; }
        public bool Enable { get; set; }
    }
}