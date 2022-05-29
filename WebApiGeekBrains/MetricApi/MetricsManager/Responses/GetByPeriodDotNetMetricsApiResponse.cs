using System.Collections.Generic;
using MetricsManager.Responses.DataTransferObjects;

namespace MetricsManager.Responses
{
    public class GetByPeriodDotNetMetricsApiResponse
    {
        public IList<DotNetMetricDto> Metrics { get; set; }
    }
}