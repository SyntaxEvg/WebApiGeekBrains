﻿using System;
using MetricsManagerClient.Data.Interfaces;

namespace MetricsManagerClient.Data.Models
{
    public class AppModel: IAppModel
    {
        public bool IsFollowAgent { get; set; }
        public string ManagerBaseAddress { get; set; } = "http://localhost:51685";
        
        public DateTimeOffset From { get; set; } = DateTimeOffset.UtcNow;
        
        public DateTimeOffset To { get; set; } = DateTimeOffset.UtcNow;
        public int AgentId { get; set; }
    }
}