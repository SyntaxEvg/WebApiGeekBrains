﻿using System.Collections.Generic;
using MetricsManager.Responses.DataTransferObjects;

namespace MetricsManager.Responses
{
    public class GetByPeriodHddMetricsApiResponse
    {
        public IList<HddMetricDto> Metrics { get; set; }
    }
}