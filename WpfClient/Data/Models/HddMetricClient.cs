﻿using System;

namespace MetricsManagerClient.Data.Models
{
    public class HddMetricClient
    {
        public int Id { get; set; } 
        public int Value { get; set; } 
        public DateTimeOffset Time { get; set; }
    }
}