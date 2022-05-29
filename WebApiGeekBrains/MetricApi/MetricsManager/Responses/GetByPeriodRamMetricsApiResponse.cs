using System.Collections.Generic;
using MetricsManager.Responses.DataTransferObjects;

namespace MetricsManager.Responses
{
    public class GetByPeriodRamMetricsApiResponse
    {
        public IList<RamMetricDto> Metrics { get; set; }
    }
}