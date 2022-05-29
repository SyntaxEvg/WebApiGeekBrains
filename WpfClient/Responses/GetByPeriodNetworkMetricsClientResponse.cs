using MetricsManagerClient.Responses.DataObjects;
using System.Collections.Generic;

namespace WpfClient.Responses
{
    public class GetByPeriodNetworkMetricsClientResponse
    {
        public List<NetworkMetricClientDto> Metrics { get; set; }
    }
}