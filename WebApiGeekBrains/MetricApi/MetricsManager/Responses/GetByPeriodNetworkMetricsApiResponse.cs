using MetricsManager.Responses.DataTransferObjects;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class GetByPeriodNetworkMetricsApiResponse
    {
        public IList<NetworkMetricDto> Metrics { get; set; }
    }
}