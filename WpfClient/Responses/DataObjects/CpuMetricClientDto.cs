﻿
using System;

namespace MetricsManagerClient.Responses.DataObjects
{
    public class CpuMetricClientDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public int AgentId { get; set; }
    }

}